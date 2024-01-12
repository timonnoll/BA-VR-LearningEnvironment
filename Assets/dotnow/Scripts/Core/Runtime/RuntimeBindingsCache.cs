﻿using dotnow;
using System;
using System.Reflection;

namespace dotnow.Runtime
{
    public class RuntimeBindingsCache
    {
        // Private
        private static object nullMatchToken = new object();

        private CLRInstance instance = null;
        private object[] proxyMemberCache = null;

        // Constructor
        public RuntimeBindingsCache(CLRInstance instance, int memberCount)
        {
            this.instance = instance;
            this.proxyMemberCache = new object[memberCount];
        }

        // Methods
        public void InvokeProxyMethod(int offset, string methodName, BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy)
        {
            Action invoke = FindProxyMethodDelegate(offset, methodName, flags);

            if (invoke != null)
                invoke();
        }

        public object InvokeProxyMethod(int offset, string methodName, object[] args, BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy)
        {
            MethodInfo info = FindProxyMethodInfo(offset, methodName, flags);

            if (info != null)
                return info.Invoke(instance, args);

            return null;
        }

        private MethodInfo FindProxyMethodInfo(int offset, string methodName, BindingFlags flags)
        {
            return FindProxyMethodToken(offset, methodName, flags) as MethodInfo;
        }

        private Action FindProxyMethodDelegate(int offset, string methodName, BindingFlags flags)
        {
            return FindProxyMethodToken(offset, methodName, flags) as Action;
        }

        private object FindProxyMethodToken(int offset, string methodName, BindingFlags flags)
        {
            object token = proxyMemberCache[offset];

            // Check for searched
            if(token != nullMatchToken && token == null)
            {
                // try to find the method
                MethodInfo method = instance.Type.GetMethod(methodName, flags);

                // Check for found
                if(method != null)
                {
                    // Check for delegate
                    if (method.ReturnType == typeof(void) && method.GetParameters().Length == 0)
                    {
                        // Create delegate
                        Action invoke = (Action)method.CreateDelegate(typeof(Action), instance);

                        // Override cached result with delegate which will have lower overhead
                        proxyMemberCache[offset] = invoke;
                        token = invoke;
                    }
                    else
                    {
                        token = method;
                    }
                }
                else
                {
                    // Searched for the member and was not found
                    proxyMemberCache[offset] = nullMatchToken;
                }
            }
            return token;
        }
    }
}

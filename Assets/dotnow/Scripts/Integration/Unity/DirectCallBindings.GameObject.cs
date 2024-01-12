﻿#if !UNITY_DISABLE
#if (UNITY_EDITOR || UNITY_STANDALONE || UNITY_IOS || UNITY_ANDROID || UNITY_WSA || UNITY_WEBGL || UNITY_SWITCH)
using dotnow;
using dotnow.Runtime;

namespace UnityEngine
{
    internal static partial class DirectCallBindings
    {
        [Preserve]
        [CLRMethodDirectCallBinding(typeof(GameObject), "get_transform")]
        public static void UnityEngine_GameObject_GetTransform(StackData[] stack, int offset)
        {
            stack[offset].refValue = ((GameObject)stack[offset].refValue).transform;
            stack[offset].type = StackData.ObjectType.Ref;
        }
    }
}
#endif
#endif
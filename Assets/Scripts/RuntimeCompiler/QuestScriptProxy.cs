using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using dotnow;

namespace TN
{
    /// <summary>
    /// Proxy class needed for integrating dotnow interpreter for android runtime compiling.
    /// </summary>
    public class QuestScriptProxy : dotnow.Interop.ICLRProxy
    {
        dotnow.AppDomain domain;
        dotnow.CLRInstance instance;
        public CLRInstance Instance => instance;

        // initialize dotnow proxy.
        public void InitializeProxy(dotnow.AppDomain domain, dotnow.CLRInstance instance)
        {
            this.domain = domain;
            this.instance = instance;
        }
    }

}

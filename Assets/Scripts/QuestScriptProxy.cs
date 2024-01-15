using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using dotnow;

namespace TN
{
    [dotnow.CLRProxyBinding(typeof(QuestScript))]
    public class QuestScriptProxy : QuestScript, dotnow.Interop.ICLRProxy
    {
        dotnow.AppDomain domain;
        dotnow.CLRInstance instance;
        public CLRInstance Instance => instance;

        public void InitializeProxy(dotnow.AppDomain domain, dotnow.CLRInstance instance)
        {
            this.domain = domain;
            this.instance = instance;
        }

        public override int GetOddNumbers()
        {
            throw new System.NotImplementedException();
        }
    }

}

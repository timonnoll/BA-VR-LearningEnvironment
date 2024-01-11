using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TN
{
    [System.Serializable]
    public class Quest
    {
        public bool isActive;
        public bool isCodingQuest;

        public string title;
        public string description;
        public string methodName;

        [Multiline(4)]
        public string variables;
        public string returnName;

        public void Complete()
        {
            isActive = false;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TN
{
    public class KeyAction : MonoBehaviour
    {
        public enum Function
        {
            Help,
            Backspace,
            Enter,
            Build,
        }

        public Function buttonFunction;
        private Button button;
        private ScriptEditor scriptEditor;



        private void Start()
        {
            button = GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(FireActionKey);

            scriptEditor = GetComponentInParent<ScriptEditor>();
        }

        private void FireActionKey()
        {
            scriptEditor.ActionKey(this);
        }
    }

}


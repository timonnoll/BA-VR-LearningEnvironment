using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TN
{
    /// <summary>
    /// Initialize action key events.
    /// </summary>
    public class KeyAction : MonoBehaviour
    {
        public enum Function
        {
            Help,
            Space,
            Backspace,
            Enter,
            SwapToMainkeys,
            SwapToSecondarykeys,
            Build,
            Next,
            Previous,
            Clear,
            Return,
            VarHelp,
            OpHelp,
            DataHelp,
            ControlHelp
        }

        public Function buttonFunction;

        private Button button;
        private ScriptEditor scriptEditor;

        // Add listener to action key.
        private void Start()
        {
            button = GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(FireActionKey);

            scriptEditor = GetComponentInParent<ScriptEditor>();
        }

        // Fire action key event if key is pressed.
        private void FireActionKey()
        {
            scriptEditor.ActionKey(this);
        }
    }

}


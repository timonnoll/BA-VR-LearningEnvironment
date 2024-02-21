using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TN
{
    /// <summary>
    /// Initialize value key events.
    /// </summary>
    public class KeyValue : MonoBehaviour
    {
        // default value for this key
        [Multiline(4)]
        public string value;

        private Button button;
        private ScriptEditor scriptEditor;

        private void Awake()
        {
            button = GetComponent<Button>();
            scriptEditor = GetComponentInParent<ScriptEditor>();
        }

        // Add listener to value key.
        private void Start()
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(onClickAppendValue);
        }

        // Fire append value event if key is pressed.
        public void onClickAppendValue()
        {
            scriptEditor.AppendValue(this);
        }

    }

}

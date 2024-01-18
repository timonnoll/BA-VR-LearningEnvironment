using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TN
{

    public class KeyValue : MonoBehaviour
    {
        // default value for this key
        [Multiline(4)]
        public string value;

        // reference to child text component
        private TextMeshProUGUI textComponent;
        private Button button;
        private ScriptEditor scriptEditor;

        private void Awake()
        {
            button = GetComponent<Button>();
            scriptEditor = GetComponentInParent<ScriptEditor>();
        }

        void Start()
        {
            textComponent = gameObject.GetComponentInChildren<TextMeshProUGUI>();
            textComponent.text = value;

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(onClickAppendValue);
        }

        public void onClickAppendValue()
        {
            scriptEditor.AppendValue(this);
        }

    }

}

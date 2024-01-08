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
        public string value;

        // reference to child text component
        private TextMeshProUGUI textComponent;
        private Button button;
        private CodingKeyboard codingKeyboard;

        private void Awake()
        {
            button = GetComponent<Button>();
            codingKeyboard = GetComponentInParent<CodingKeyboard>();
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
            codingKeyboard.AppendValue(this);
        }

    }

}

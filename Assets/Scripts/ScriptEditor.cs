using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TN
{
    public class ScriptEditor : MonoBehaviour
    {
        /// Event fired when char key on keyboard is pressed.
        public event Action<KeyValue> OnKeyValuePressed = delegate { };

        public event Action<KeyAction> OnKeyActionPressed = delegate { };
        public TMP_InputField inputField;
        public Image Keyboard;

        public ScriptBuilder scriptBuilder;

        private int currentCaretPosition = 0;


        public void AppendValue(KeyValue keyValue)
        {
            string value = "";

            OnKeyValuePressed(keyValue);

            value = keyValue.value;

            currentCaretPosition = inputField.caretPosition;
            inputField.text = inputField.text.Insert(currentCaretPosition, value);
            currentCaretPosition += value.Length;

            UpdateCaretPosition(currentCaretPosition);
        }

        public void Help()
        {

        }

        public void Enter()
        {
            string enterString = "\n";

            currentCaretPosition = inputField.caretPosition;

            inputField.text = inputField.text.Insert(currentCaretPosition, enterString);
            currentCaretPosition += enterString.Length;

            UpdateCaretPosition(currentCaretPosition);
        }

        private void UpdateCaretPosition(int newPos) => inputField.caretPosition = newPos;

        public void Backspace()
        {

        }
        public void Build()
        {
            scriptBuilder.CreateAndCompileScript(inputField.text);
        }

        public void ActionKey(KeyAction keyAction)
        {
            OnKeyActionPressed(keyAction);

            switch (keyAction.buttonFunction)
            {
                case KeyAction.Function.Help:
                    {
                        Help();
                        break;
                    }

                case KeyAction.Function.Enter:
                    {
                        Enter();
                        break;
                    }

                case KeyAction.Function.Backspace:
                    {
                        Backspace();
                        break;
                    }

                case KeyAction.Function.Build:
                    {
                        Build();
                        break;
                    }

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace TN
{
    /// <summary>
    /// Editor to write C#-code ingame and display assistance.
    /// </summary>
    public class ScriptEditor : MonoBehaviour
    {
        // Event fired when key on script editor is pressed.
        public event Action<KeyValue> OnKeyValuePressed = delegate { };
        public event Action<KeyAction> OnKeyActionPressed = delegate { };

        public TMP_InputField inputField;

        [Header("Keyboard Buttons")]
        public GameObject keyboard;
        public GameObject helpboard;
        public GameObject secondaryKeyboard;

        [Header("Help Pages")]
        public GameObject variableHelp;
        public GameObject operationHelp;
        public GameObject dataStructureHelp;
        public GameObject controlStructureHelp;

        public ScriptBuilder scriptBuilder;

        private int currentCaretPosition = 0;

        private bool buildValid = false;

        // Enable caret selection via ray interactor.
        private void Start()
        {
            inputField.shouldHideMobileInput = true;

            keyboard.SetActive(true);
            helpboard.SetActive(false);
            secondaryKeyboard.SetActive(false);
        }

        // Update Caret Position after input or function.
        private void UpdateCaretPosition(int newPos) => inputField.caretPosition = newPos;

        // Add value to coding input field.
        public void AppendValue(KeyValue keyValue)
        {
            AudioManager.instance.Play("Select");

            string value = "";

            OnKeyValuePressed(keyValue);

            value = keyValue.value;

            currentCaretPosition = inputField.caretPosition;
            inputField.text = inputField.text.Insert(currentCaretPosition, value);
            currentCaretPosition += value.Length;
            UpdateCaretPosition(currentCaretPosition);

            if (value == "{" || value == "}" || value == ";")
                Enter();

        }

        // Call selected Action.
        public void ActionKey(KeyAction keyAction)
        {
            AudioManager.instance.Play("Select");

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

                case KeyAction.Function.Space:
                    {
                        Space();
                        break;
                    }
                case KeyAction.Function.SwapToMainkeys:
                    {
                        Swap(true);
                        break;
                    }
                case KeyAction.Function.SwapToSecondarykeys:
                    {
                        Swap(false);
                        break;
                    }

                case KeyAction.Function.Next:
                    {
                        MoveCaretRight();
                        break;
                    }

                case KeyAction.Function.Previous:
                    {
                        MoveCaretLeft();
                        break;
                    }
                case KeyAction.Function.Clear:
                    {
                        Clear();
                        break;
                    }
                case KeyAction.Function.Return:
                    {
                        Return();
                        break;
                    }
                case KeyAction.Function.VarHelp:
                    {
                        ShowHelp("varhelp");
                        break;
                    }
                case KeyAction.Function.OpHelp:
                    {
                        ShowHelp("ophelp");
                        break;
                    }
                case KeyAction.Function.DataHelp:
                    {
                        ShowHelp("datahelp");
                        break;
                    }
                case KeyAction.Function.ControlHelp:
                    {
                        ShowHelp("controlhelp");
                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        // Show assistance selection.
        public void Help()
        {
            keyboard.SetActive(false);
            secondaryKeyboard.SetActive(false);
            helpboard.SetActive(true);
            scriptBuilder.questSystem.HideQuest();
        }

        // Switch view from assistance pages to coding display.
        public void Return()
        {

            HideHelp();
            helpboard.SetActive(false);
            keyboard.SetActive(true);
            scriptBuilder.questSystem.ShowQuest();
        }

        // Swaps certain keys.
        public void Swap(bool swap)
        {
            if (swap)
            {
                keyboard.SetActive(true);
                secondaryKeyboard.SetActive(false);
            }
            else
            {
                keyboard.SetActive(false);
                secondaryKeyboard.SetActive(true);
            }
        }

        // Show assistance pages.
        public void ShowHelp(string content)
        {
            HideHelp();

            switch (content)
            {
                case "varhelp":
                    {
                        variableHelp.SetActive(true);
                        break;
                    }

                case "ophelp":
                    {
                        operationHelp.SetActive(true);
                        break;
                    }

                case "datahelp":
                    {
                        dataStructureHelp.SetActive(true);
                        break;
                    }

                case "controlhelp":
                    {
                        controlStructureHelp.SetActive(true);
                        break;
                    }
            }
        }

        // Hide assistance pages.
        public void HideHelp()
        {
            variableHelp.SetActive(false);
            operationHelp.SetActive(false);
            dataStructureHelp.SetActive(false);
            controlStructureHelp.SetActive(false);
        }

        // Do a line break.
        public void Enter()
        {
            string enterString = "\n";

            currentCaretPosition = inputField.caretPosition;

            inputField.text = inputField.text.Insert(currentCaretPosition, enterString);
            currentCaretPosition += enterString.Length;

            UpdateCaretPosition(currentCaretPosition);
        }

        // Delete a character.
        public void Backspace()
        {
            currentCaretPosition = inputField.caretPosition;

            if (currentCaretPosition > 0)
            {
                --currentCaretPosition;
                inputField.text = inputField.text.Remove(currentCaretPosition, 1);
                UpdateCaretPosition(currentCaretPosition);
            }
        }

        // Call compiling function and build the playerwritten script. 
        public void Build()
        {
            buildValid = scriptBuilder.CreateAndCompileScript(inputField.text);

            if (buildValid)
                Clear();
        }

        // Insert a space character.
        public void Space()
        {
            currentCaretPosition = inputField.caretPosition;
            inputField.text = inputField.text.Insert(currentCaretPosition++, " ");

            UpdateCaretPosition(currentCaretPosition);
        }

        // Move caret to the left.
        public void MoveCaretLeft()
        {
            currentCaretPosition = inputField.caretPosition;

            if (currentCaretPosition > 0)
            {
                --currentCaretPosition;
                UpdateCaretPosition(currentCaretPosition);
            }
        }

        // Move caret to the right.
        public void MoveCaretRight()
        {
            currentCaretPosition = inputField.caretPosition;

            if (currentCaretPosition < inputField.text.Length)
            {
                ++currentCaretPosition;
                UpdateCaretPosition(currentCaretPosition);
            }
        }

        // Clear coding input field.
        public void Clear()
        {
            if (inputField.caretPosition != 0)
            {
                inputField.MoveTextStart(false);
            }
            inputField.text = "";
            currentCaretPosition = inputField.caretPosition;
        }


    }

}

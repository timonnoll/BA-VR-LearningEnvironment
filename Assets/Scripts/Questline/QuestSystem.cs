using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RoslynCSharp;
using RoslynCSharp.Compiler;
using System;
using UnityEngine.Timeline;

namespace TN
{
    /// <summary>
    /// Handling and evaluating Quests.
    /// </summary>
    public class QuestSystem : MonoBehaviour
    {
        public Quest[] quests;


        [Header("UI Displays")]
        public GameObject questWindow;
        public GameObject codingWindow;

        [Header("Active Quest")]
        public TextMeshProUGUI activeTitle;
        public TextMeshProUGUI activeDescription;
        public TextMeshProUGUI activeMethodName;
        public TextMeshProUGUI activeVariables;
        public TextMeshProUGUI activeReturnName;

        public TextMeshProUGUI consoleField;

        public DialogueSystem dialogueSystem;

        public Animator doorAnimator;

        private int activeQuest = 0;

        // Display the active quest.
        public void ShowQuest()
        {
            Quest quest = GetActiveQuest();

            quest.isActive = true;

            activeTitle.text = quest.title;
            activeDescription.text = quest.description;

            questWindow.SetActive(true);

            if (quest.isCodingQuest)
            {
                activeMethodName.text = quest.methodName;
                activeVariables.text = quest.variables;
                activeReturnName.text = quest.returnName;

                codingWindow.SetActive(true);
            }
            else
                codingWindow.SetActive(false);

        }

        // Hide the active quest.
        public void HideQuest()
        {
            questWindow.SetActive(false);
            codingWindow.SetActive(false);
        }

        // Update the active quest if complete and show the next quest.
        public void UpdateActiveQuest()
        {
            GetActiveQuest().Complete();
            //Complete Sound?
            if (quests[activeQuest + 1] != null)
            {
                activeQuest += 1;
                ShowQuest();
            }
            else
            {
                codingWindow.SetActive(false);
                activeTitle.text = "Congratulations!";
                activeDescription.text = "You have completed all tasks";
            }
        }

        // Get the active quest object.
        public Quest GetActiveQuest()
        {
            return quests[activeQuest];
        }

        // Evaluate the scan quest.
        public void EvaluateScanQuest()
        {
            if (activeQuest == 1)
            {
                UpdateActiveQuest();
                dialogueSystem.StartDialog(2);
            }
        }

        // Evaluate the energy quest.
        public void EvaluateEnergyQuest()
        {
            if (activeQuest == 4)
            {
                UpdateActiveQuest();
                dialogueSystem.StartDialog(5);
            }
        }

        // Evaluate the scripting quests by calling the ingame written functions.
        public bool EvaluateQuestScript(ScriptProxy activeQuestScript)
        {
            Quest quest = GetActiveQuest();

            switch (quest.title)
            {
                case "Orbit Algorithmus":
                    {
                        if ((bool)activeQuestScript.Call("OrbitControl", 150) && (bool)activeQuestScript.Call("OrbitControl", 60)
                        && !(bool)activeQuestScript.Call("OrbitControl", 12) && !(bool)activeQuestScript.Call("OrbitControl", 5))
                        {
                            UpdateActiveQuest();
                            dialogueSystem.StartDialog(3);
                            return true;
                        }
                        else
                        {
                            consoleField.text = "The function does not work as intended.";
                            return false;
                        }
                    }
                case "Pincode":
                    {
                        List<int> input = new List<int> { 10, 1, 7, 6, 5, 9, 30, 12 };
                        List<int> result;

                        result = (List<int>)activeQuestScript.Call("GetOddNumbers", input);
                        if (result.Count == 4 && result.Contains(1) && result.Contains(7) && result.Contains(5) && result.Contains(9))
                        {
                            UpdateActiveQuest();
                            dialogueSystem.StartDialog(4);

                            doorAnimator.SetBool("Open", true);
                            AudioManager.instance.Play("DoorQuest");

                            return true;
                        }
                        else
                        {
                            consoleField.text = "The function does not work as intended.";
                            Debug.Log("false");
                            return false;
                        }
                    }
                case "Durchschnittsberechnung":
                    {
                        List<int> input = new List<int> { 10, 1, 7, 6, 5, 9, 30, 12 };

                        if ((double)activeQuestScript.Call("GetAverage", input) == 10.0)
                        {
                            UpdateActiveQuest();
                            dialogueSystem.StartDialog(6);
                            return true;
                        }
                        else
                        {
                            consoleField.text = "The function does not work as intended.";
                            return false;
                        }
                    }
                default:
                    throw new Exception("Can't find Method");
            }
        }

    }

}

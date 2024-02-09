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
    public class QuestSystem : MonoBehaviour
    {
        public Quest[] quests;

        public NPCInteractable npcInteractable;

        public GameObject questWindow;
        public GameObject codingWindow;

        public TextMeshProUGUI activeTitle;
        public TextMeshProUGUI activeDescription;
        public TextMeshProUGUI activeMethodName;
        public TextMeshProUGUI activeVariables;
        public TextMeshProUGUI activeReturnName;

        public TextMeshProUGUI consoleField;

        public Animator doorAnimator;

        private int activeQuest = 0;

        private int scans = 0;

        List<int> result = new List<int>();
        List<int> input = new List<int> { 10, 1, 7, 6, 5, 9, 30, 12 };

        public void ShowQuest()
        {
            Quest quest = quests[activeQuest];

            quests[activeQuest].isActive = true;

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

        public void HideQuest()
        {
            questWindow.SetActive(false);
            codingWindow.SetActive(false);
        }

        public void UpdateActiveQuest()
        {
            quests[activeQuest].Complete();
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

        public Quest GetActiveQuest()
        {
            return quests[activeQuest];
        }

        public void EvaluateScanQuest()
        {
            scans++;
            if (scans == 1)
            {
                UpdateActiveQuest();
                npcInteractable.StartDialog(2);
            }
        }

        public void EvaluateEnergyQuest()
        {
            UpdateActiveQuest();
            npcInteractable.StartDialog(5);
        }

        public bool EvaluateQuestScript(ScriptProxy activeQuestScript)
        {
            Quest quest = GetActiveQuest();

            switch (quest.title)
            {
                case "Orbit Control":
                    {
                        if ((bool)activeQuestScript.Call("OrbitControl", 150) && (bool)activeQuestScript.Call("OrbitControl", 60)
                        && !(bool)activeQuestScript.Call("OrbitControl", 12) && !(bool)activeQuestScript.Call("OrbitControl", 5))
                        {
                            UpdateActiveQuest();
                            npcInteractable.StartDialog(3);
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

                        result = (List<int>)activeQuestScript.Call("GetOddNumbers", input);
                        if (result.Count == 4 && result.Contains(1) && result.Contains(7) && result.Contains(5) && result.Contains(9))
                        {
                            UpdateActiveQuest();
                            npcInteractable.StartDialog(4);

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
                case "Average Calculation":
                    {
                        if ((double)activeQuestScript.Call("GetAverage", input) == 8.0)
                        {
                            UpdateActiveQuest();
                            npcInteractable.StartDialog(6);
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

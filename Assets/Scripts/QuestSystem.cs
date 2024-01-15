using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RoslynCSharp;
using RoslynCSharp.Compiler;
using System;

namespace TN
{
    public class QuestSystem : MonoBehaviour
    {
        public Quest[] quests;

        public GameObject questWindow;
        public GameObject codingWindow;

        public TextMeshProUGUI activeTitle;
        public TextMeshProUGUI activeDescription;
        public TextMeshProUGUI activeMethodName;
        public TextMeshProUGUI activeVariables;
        public TextMeshProUGUI activeReturnName;


        private int activeQuest = 0;

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

        public void UpdateActiveQuest()
        {
            quests[activeQuest].Complete();
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

        public void EvaluateQuestScript(ScriptProxy activeQuestScript)
        {
            Quest quest = GetActiveQuest();
            int result;

            switch (quest.title)
            {
                case "Pincode":
                    {
                        result = (int)activeQuestScript.Call("GetOddNumbers");
                        if (result == 5)
                        {
                            UpdateActiveQuest();
                            Debug.Log("true");
                        }
                        Debug.Log("false");
                        break;
                    }
                default:
                    throw new Exception("Can't find Method");
            }
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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


        private int questProgress = 0;

        public void ShowQuest()
        {
            quests[questProgress].isActive = true;
            activeTitle.text = quests[questProgress].title;
            activeDescription.text = quests[questProgress].description;

            questWindow.SetActive(true);

            if (quests[questProgress].isCodingQuest)
            {
                activeMethodName.text = quests[questProgress].methodName;
                activeVariables.text = quests[questProgress].variables;
                activeReturnName.text = quests[questProgress].returnName;

                codingWindow.SetActive(true);
            }
            else
                codingWindow.SetActive(false);

        }

        public void UpdateQuestProgress()
        {
            quests[questProgress].Complete();
            if (quests[questProgress + 1] != null)
            {
                questProgress += 1;
                ShowQuest();
            }
            else
            {
                codingWindow.SetActive(false);
                activeTitle.text = "Congratulations!";
                activeDescription.text = "You have completed all tasks";
            }
        }

    }

}

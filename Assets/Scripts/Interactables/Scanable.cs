using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TN
{
    public class Scanable : MonoBehaviour
    {
        public QuestSystem questSystem;
        public GameObject scanData;

        public float disableDisplayTime = 3;
        private float timer = 0;

        private int scanCount = 0;

        void Start()
        {
            scanData.SetActive(false);
        }
        public void ShowInfo()
        {
            scanData.SetActive(true);

            scanCount++;
            if (scanCount == 1)
                questSystem.EvaluateScanQuest();

            timer += Time.deltaTime;
            if (timer > disableDisplayTime)
            {
                scanData.SetActive(false);
            }
        }
    }

}

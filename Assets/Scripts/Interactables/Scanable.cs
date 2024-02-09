using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TN
{
    public class Scanable : MonoBehaviour
    {
        public QuestSystem questSystem;

        public float disableDisplayTime = 3;

        private GameObject scanData;
        private float timer;
        private bool isActive;

        private int scanCount;

        private void Awake()
        {
            scanData = GameObject.Find("ScanDataCanvas");
        }

        void Update()
        {
            if (isActive)
            {
                timer += Time.deltaTime;
                if (timer > disableDisplayTime)
                {
                    scanData.SetActive(false);
                    isActive = false;
                }
            }
            else
                timer = 0;

        }

        void Start()
        {
            scanData.SetActive(false);
            isActive = false;
            timer = 0;
            scanCount = 0;
        }

        public void ShowInfo()
        {
            isActive = true;
            scanData.SetActive(true);

            scanCount++;

            if (scanCount == 1)
                questSystem.EvaluateScanQuest();
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TN
{
    /// <summary>
    /// Class for objects that are scanable
    /// </summary>
    public class Scanable : MonoBehaviour
    {
        public GameObject scanData;
        public QuestSystem questSystem;

        public float disableDisplayTime = 3;

        private float timer;
        private bool isActive;

        // Initialize variables at start.
        private void Start()
        {
            scanData.SetActive(false);
            isActive = false;
            timer = 0;
        }

        // Check whether scan data is active if so, deactivate after specified seconds.
        private void Update()
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


        // Function is called by ObjectScanner. Set scan data to active.
        public void ShowInfo()
        {
            isActive = true;
            scanData.SetActive(true);

            questSystem.EvaluateScanQuest();
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TN
{
    /// <summary>
    /// Handle energy status of objects if battery interacts with capacitor socket.
    /// </summary>
    public class ActivateEnergy : MonoBehaviour
    {
        public ButtonPushOpenDoor button;
        public QuestSystem questSystem;
        private void Start()
        {
            GetComponent<XRSocketTagInteractor>().selectEntered.AddListener(x => SetEnergyActive());
            GetComponent<XRSocketTagInteractor>().selectExited.AddListener(x => SetEnergyInactive());
        }

        // Set energy to active and evalute energy quest. 
        public void SetEnergyActive()
        {
            button.SetEnergy(true);
            questSystem.EvaluateEnergyQuest();
        }
        // Set energy to inactive.
        public void SetEnergyInactive()
        {
            button.SetEnergy(false);
        }

    }
}

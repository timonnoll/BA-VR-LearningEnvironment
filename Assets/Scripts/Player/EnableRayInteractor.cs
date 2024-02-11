using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TN
{
    /// <summary>
    /// Enables ray interactors while triggering a specific collider.
    /// </summary>
    public class EnableRayInteractor : MonoBehaviour
    {
        public GameObject rightRayInteractor;
        public GameObject leftRayInteractor;

        // Deactivate at game start.
        private void Start()
        {
            rightRayInteractor.SetActive(false);
            leftRayInteractor.SetActive(false);
        }

        // Activate by entering the collider.
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                rightRayInteractor.SetActive(true);
                leftRayInteractor.SetActive(true);
            }
        }

        // Deactivate by leaving the collider.
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                rightRayInteractor.SetActive(false);
                leftRayInteractor.SetActive(false);
            }
        }
    }
}

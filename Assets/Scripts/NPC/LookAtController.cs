using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TN
{
    /// <summary>
    /// Animate head to look at object (player).
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class LookAtController : MonoBehaviour
    {
        public Transform objectToLookAt;
        public float headWeight;
        public float bodyWeight;

        private bool isActive;
        private Animator animator;


        // Get animator and set start activation status.
        void Start()
        {
            isActive = false;
            animator = GetComponent<Animator>();
        }

        // Call animator to look at given position and set intensity.
        private void OnAnimatorIK(int layerIndex)
        {
            if (isActive)
            {
                animator.SetLookAtPosition(objectToLookAt.position);
                animator.SetLookAtWeight(1, bodyWeight, headWeight);
            }
        }

        // Set activation status.
        public void SetStatus(bool status)
        {
            isActive = status;
        }
    }

}

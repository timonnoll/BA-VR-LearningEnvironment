using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TN
{
    [RequireComponent(typeof(Animator))]
    public class LookAtController : MonoBehaviour
    {
        public Transform objectToLookAt;
        public float headWeight;
        public float bodyWeight;
        private bool isActive;
        private Animator animator;


        void Start()
        {
            isActive = false;
            animator = GetComponent<Animator>();
        }
        private void OnAnimatorIK(int layerIndex)
        {
            if (isActive)
            {
                animator.SetLookAtPosition(objectToLookAt.position);
                animator.SetLookAtWeight(1, bodyWeight, headWeight);
            }
        }

        public void SetStatus(bool status)
        {
            isActive = status;
        }
    }

}

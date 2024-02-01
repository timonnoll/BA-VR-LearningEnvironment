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
        private Animator animator;


        void Start()
        {
            animator = GetComponent<Animator>();
        }
        private void OnAnimatorIK(int layerIndex)
        {
            animator.SetLookAtPosition(objectToLookAt.position);
            animator.SetLookAtWeight(1, bodyWeight, headWeight);
        }
    }

}

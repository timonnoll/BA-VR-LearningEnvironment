using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

namespace TN
{
    /// <summary>
    /// Controls the space environment with lever and wheel.
    /// </summary>
    public class SpaceEnvironmentController : MonoBehaviour
    {
        public XRLever lever;
        public XRKnob knob;

        public float forwardSpeed;
        public float sideSpeed;

        // Transform the space environment over time, depending on the XR Interactable inputs.
        private void Update()
        {
            float forwardVelocity = forwardSpeed * (lever.value ? 1 : 0);
            float sideVelocity = sideSpeed * (lever.value ? 1 : 0) * Mathf.Lerp(-1, 1, knob.value);

            Vector3 velocity = new Vector3(sideVelocity, 0, forwardVelocity);
            transform.position += velocity * Time.deltaTime;
        }
    }
}


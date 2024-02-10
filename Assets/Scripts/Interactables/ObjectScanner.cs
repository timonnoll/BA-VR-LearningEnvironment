using System.Collections;
using System.Collections.Generic;
using TN;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

namespace TN
{
    /// <summary>
    /// Scanner class used to interact with scanable objects.
    /// </summary>
    public class ObjectScanner : MonoBehaviour
    {

        public LayerMask layerMask;
        public Transform scanSource;
        public float distance = 10;

        private bool rayActivate = false;

        // Add listener to call a function when the trigger button is pressed while holding a grabable object.
        private void Start()
        {
            XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
            grabInteractable.activated.AddListener(x => StartScan());
            grabInteractable.deactivated.AddListener(x => StopScan());
        }

        // Call Function if trigger button is pressed.
        private void Update()
        {
            if (rayActivate)
                RaycastCheck();
        }

        // Set the scanner to enabled.
        public void StartScan()
        {
            AudioManager.instance.Play("Scan");
            rayActivate = true;
        }

        // Set the scanner to disabled.
        public void StopScan()
        {
            rayActivate = false;
        }

        // Check whether the raycast hits an object and if so, send it the message "Show Info".
        void RaycastCheck()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(scanSource.position, scanSource.forward, out hit, distance, layerMask);

            if (hasHit)
            {
                hit.transform.gameObject.SendMessage("ShowInfo", SendMessageOptions.DontRequireReceiver);
            }

        }
    }
}


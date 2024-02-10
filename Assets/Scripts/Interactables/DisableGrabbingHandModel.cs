using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

namespace TN
{
    /// <summary>
    /// Disable hand model while grabbing onto an object.
    /// </summary>
    public class DisableGrabbingHandModel : MonoBehaviour
    {
        public GameObject leftHandModel;
        public GameObject rightHandModel;

        // Call functions for grab events.
        private void Start()
        {
            XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
            grabInteractable.selectEntered.AddListener(HideGrabbingHand);
            grabInteractable.selectExited.AddListener(ShowGrabbingHand);

        }

        // hide hand model which grabs the object.
        public void HideGrabbingHand(SelectEnterEventArgs args)
        {
            if (args.interactorObject.transform.tag == "Left Hand")
            {
                leftHandModel.SetActive(false);
            }
            else if (args.interactorObject.transform.tag == "Right Hand")
            {
                rightHandModel.SetActive(false);
            }
        }

        // show hand model after releasing the object.
        public void ShowGrabbingHand(SelectExitEventArgs args)
        {
            if (args.interactorObject.transform.tag == "Left Hand")
            {
                leftHandModel.SetActive(true);
            }
            else if (args.interactorObject.transform.tag == "Right Hand")
            {
                rightHandModel.SetActive(true);
            }
        }
    }
}

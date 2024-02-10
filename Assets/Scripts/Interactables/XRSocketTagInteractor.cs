using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace TN
{
    /// <summary>
    /// Override XRSocketInteractor functions. Makes the Socket Interactor check for the targetTag. 
    /// </summary>
    public class XRSocketTagInteractor : XRSocketInteractor
    {
        public string targetTag;

        public override bool CanHover(IXRHoverInteractable interactable)
        {
            return base.CanHover(interactable) && interactable.transform.tag == targetTag;
        }

        public override bool CanSelect(IXRSelectInteractable interactable)
        {
            return base.CanSelect(interactable) && interactable.transform.tag == targetTag;
        }
    }
}

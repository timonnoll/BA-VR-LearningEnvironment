using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.State;

namespace TN
{
    /// <summary>
    /// Bind interactable source of AffordanceStateProvider to interactable parent component.
    /// </summary>
    public class FindInteractableAffordance : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<XRInteractableAffordanceStateProvider>().interactableSource = GetComponentInParent<XRBaseInteractable>();
        }
    }
}


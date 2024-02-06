using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

namespace TN
{
    public class PlayerInteract : MonoBehaviour
    {
        public InputActionReference selectInputActionReference;


        public void Update()
        {
            if (selectInputActionReference.action.ReadValue<bool>())
            {
                float interactRange = 2f;
                Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
                foreach (Collider collider in colliderArray)
                {
                    if (collider.TryGetComponent(out NPCInteractable npcInteractable))
                    {
                        npcInteractable.Interact();
                    }
                }
            }

        }

        public NPCInteractable GetInteractableObject()
        {
            float interactRange = 2f;
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliderArray)
            {
                if (collider.TryGetComponent(out NPCInteractable npcInteractable))
                {
                    return npcInteractable;
                }
            }
            return null;
        }
    }
}

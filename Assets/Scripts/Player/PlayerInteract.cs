using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TN
{
    /// <summary>
    /// Handle collision-related interactions on button input.
    /// </summary>
    public class PlayerInteract : MonoBehaviour
    {
        public InputActionReference selectInputActionReference;

        // check if interactable object is nearby, if button is pressed and then call a function.
        private void Update()
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

        // check for interactable NPCs nearby.
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

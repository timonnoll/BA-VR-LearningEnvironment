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
        public InputActionProperty selectInputAction;

        // check if interactable object is nearby, if button is pressed and then call a function.
        private void Update()
        {
            if (selectInputAction.action.ReadValue<float>() == 1)
            {
                float interactRange = 3f;
                Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
                foreach (Collider collider in colliderArray)
                {
                    if (collider.TryGetComponent(out NPCInteractable npcInteractable))
                    {
                        npcInteractable.Interact();
                        selectInputAction.action.Reset();
                    }
                }
            }

        }

        // check for interactable NPCs nearby.
        public NPCInteractable GetInteractableObject()
        {
            float interactRange = 3f;
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

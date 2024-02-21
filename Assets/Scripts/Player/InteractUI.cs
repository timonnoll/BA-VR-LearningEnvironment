using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TN
{
    /// <summary>
    /// Visualize possible player interaction.
    /// </summary>
    public class InteractUI : MonoBehaviour
    {
        public GameObject buttonCanvas;
        public PlayerInteract playerInteract;
        public LookAtController lookAtController;


        // Call a function to check if interactable object (NPC) is nearby.
        private void Update()
        {
            if (playerInteract.GetInteractableObject() != null)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        // Show required controller input (and let the npc and button look at the player).
        private void Show()
        {
            lookAtController.SetStatus(true);
            buttonCanvas.SetActive(true);
            transform.LookAt(new Vector3(lookAtController.objectToLookAt.position.x,
                lookAtController.objectToLookAt.position.y + 1.7f, lookAtController.objectToLookAt.position.z));
        }

        // Hide required controller input (and stop the npc from looking at the player).
        private void Hide()
        {
            lookAtController.SetStatus(false);
            buttonCanvas.SetActive(false);
        }
    }
}

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

        // Show required controller input (and let the npc look at the player).
        private void Show()
        {
            lookAtController.SetStatus(true);
            buttonCanvas.SetActive(true);
        }

        // Hide required controller input (and stop the npc from looking at the player).
        private void Hide()
        {
            lookAtController.SetStatus(false);
            buttonCanvas.SetActive(false);
        }
    }
}

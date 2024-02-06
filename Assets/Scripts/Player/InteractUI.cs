using System.Collections;
using System.Collections.Generic;
using TN;
using UnityEngine;

namespace TN
{
    public class InteractUI : MonoBehaviour
    {
        [SerializeField] private GameObject buttonCanvas;
        [SerializeField] private PlayerInteract playerInteract;
        [SerializeField] private LookAtController lookAtController;

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

        private void Show()
        {
            lookAtController.SetStatus(true);
            buttonCanvas.SetActive(true);
        }
        private void Hide()
        {
            lookAtController.SetStatus(false);
            buttonCanvas.SetActive(false);
        }
    }
}

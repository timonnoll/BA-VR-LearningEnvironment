using System.Collections;
using System.Collections.Generic;
using TN;
using UnityEngine;

namespace TN
{
    public class ButtonInteractUI : MonoBehaviour
    {
        [SerializeField] private GameObject buttonCanvas;
        [SerializeField] private PlayerInteract playerInteract;

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
            buttonCanvas.SetActive(true);
        }
        private void Hide()
        {
            buttonCanvas.SetActive(false);
        }
    }
}

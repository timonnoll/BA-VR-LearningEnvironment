using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace TN
{
    /// <summary>
    /// Open the door when the power source is activated and the button is pressed.
    /// </summary>
    public class ButtonPushOpenDoor : MonoBehaviour
    {
        public Animator animator;
        public string boolName = "Open";
        private bool energy;

        // Add listener to call function if button is pressed.
        void Start()
        {
            energy = false;
            GetComponent<XRSimpleInteractable>().selectEntered.AddListener(x => ToggleDoorOpen());
        }

        // toggle door state if power source is activated
        public void ToggleDoorOpen()
        {
            if (energy)
            {
                bool isOpen = animator.GetBool(boolName);
                animator.SetBool(boolName, !isOpen);

                AudioManager.instance.Play("DoorButton");
            }
        }

        // Set power source to activated.
        public void SetEnergy(bool state)
        {
            energy = state;
        }
    }
}

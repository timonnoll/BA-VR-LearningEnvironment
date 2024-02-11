using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace TN
{
    /// <summary>
    /// Handle NPC interaction.
    /// </summary>
    public class NPCInteractable : MonoBehaviour
    {
        public DialogueSystem dialogueSystem;

        private Animator animator;
        private bool startDialogSystem;

        // Get animator from component.
        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        // Allow starting the first dialog. 
        private void Start()
        {
            startDialogSystem = true;
        }

        // Start the dialogue if it is the first interaction. 
        public void Interact()
        {
            animator.Play("Waving");
            if (startDialogSystem)
            {
                startDialogSystem = false;
                dialogueSystem.StartDialog(0);
            }
        }
    }
}

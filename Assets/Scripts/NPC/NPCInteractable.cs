using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace TN
{
    [System.Serializable]
    public class Dialogue
    {
        public string[] text;
    }

    public class NPCInteractable : MonoBehaviour
    {
        public Dialogue[] dialogues;
        public TextMeshProUGUI textComponent;
        public GameObject dialogCanvas;
        public PlaySteps playSteps;

        public float pauseTimer = 2;
        public float textSpeed;

        private int activeSequence = 0;
        private int index = 0;
        private string[] lines;

        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        void Start()
        {
            dialogCanvas.SetActive(false);
            textComponent.text = string.Empty;
            lines = dialogues[activeSequence].text;
        }

        void Update()
        {
            float timer = 0;

            if (textComponent.text == lines[index])
            {
                timer += Time.deltaTime;
                if (timer > pauseTimer)
                    NextLine();
            }
        }

        public void Interact()
        {
            if (activeSequence == 0)
            {
                StartDialog(0);
            }
            animator.SetTrigger("Wave");
        }

        public void StartDialog(int playStepIndex)
        {
            gameObject.SetActive(true);
            playSteps.PlayStepIndex(playStepIndex);
            lines = dialogues[activeSequence].text;
            index = 0;
            StartCoroutine(TypeLine());
        }

        IEnumerator TypeLine()
        {
            foreach (char c in lines[index].ToCharArray())
            {
                textComponent.text += c;
                yield return new WaitForSeconds(textSpeed);
            }
        }

        void NextLine()
        {
            if (index < lines.Length - 1)
            {
                index++;
                textComponent.text = string.Empty;
                StartCoroutine(TypeLine());
            }
            else
            {
                activeSequence++;
                gameObject.SetActive(false);
            }
        }

    }
}

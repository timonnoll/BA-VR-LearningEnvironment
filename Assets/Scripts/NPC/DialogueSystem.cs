using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace TN
{
    /// <summary>
    /// Blueprint for dialogue text passages.
    /// </summary>
    [System.Serializable]
    public class Dialogue
    {
        public string[] text;
    }

    /// <summary>
    /// Call voicelines and show the appropriate dialogues.
    /// </summary>
    public class DialogueSystem : MonoBehaviour
    {
        [Header("Dialog content")]
        public Dialogue[] dialogues;
        public PlaySteps playSteps;

        [Header("Display Component")]
        public TextMeshProUGUI textComponent;
        public GameObject dialogCanvas;

        [Header("Display Properties")]
        public float pauseTimer = 2;
        public float textSpeed;

        private int activeSequence;
        private string[] lines;
        private int index;
        private float timer;

        // Set start properties of dialogue system.
        private void Start()
        {
            dialogCanvas.SetActive(false);
            textComponent.text = string.Empty;
            lines = dialogues[activeSequence].text;
            activeSequence = 0;
            index = 0;
            timer = 0;
        }

        // If the text in the dialogue box is written out, wait before starting the next line.
        private void Update()
        {
            if (textComponent.text == lines[index])
            {
                timer += Time.deltaTime;
                if (timer > pauseTimer)
                    NextLine();
            }
        }

        // Start Dialogue und play voiceline.
        public void StartDialog(int playStepIndex)
        {
            dialogCanvas.SetActive(true);
            playSteps.PlayStepIndex(playStepIndex);

            StartCoroutine(TypeLine());
        }

        // Type every character in selected line over time.
        private IEnumerator TypeLine()
        {
            foreach (char c in lines[index].ToCharArray())
            {
                textComponent.text += c;
                yield return new WaitForSeconds(textSpeed);
            }
        }

        // If available select next line, otherwise select next dialogue sequence.
        private void NextLine()
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
                textComponent.text = string.Empty;
                index = 0;
                timer = 0;
                lines = new string[dialogues[activeSequence].text.Length];
                lines = dialogues[activeSequence].text;
                dialogCanvas.SetActive(false);
            }
        }
    }

}

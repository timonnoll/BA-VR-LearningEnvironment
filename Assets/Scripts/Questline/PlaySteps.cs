using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace TN
{
    /// <summary>
    /// Blueprint for voiceline steps.
    /// </summary>
    [System.Serializable]
    public class Step
    {
        public string name;
        public float time;
        public bool hasPlayed = false;
    }

    /// <summary>
    /// Handling the voiceline steps.
    /// </summary>
    public class PlaySteps : MonoBehaviour
    {
        PlayableDirector director;
        public List<Step> steps;

        // Get the PlayableDirector from the object. 
        private void Start()
        {
            director = GetComponent<PlayableDirector>();
        }

        // Play voiceline step by given index.
        public void PlayStepIndex(int index)
        {
            Step step = steps[index];

            if (!step.hasPlayed)
            {
                step.hasPlayed = true;

                director.Stop();
                director.time = step.time;
                director.Play();
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TN
{
    /// <summary>
    /// Blueprint for initialization of SFX.
    /// </summary>
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        public AudioSource source;

        [Range(0, 1)]
        public float volume = 1;

        public bool loop = false;
        public bool playOnAwake = false;

    }

}

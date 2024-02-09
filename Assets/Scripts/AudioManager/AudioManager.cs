using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TN
{
    public class AudioManager : MonoBehaviour
    {
        public Sound[] sounds;

        public static AudioManager instance;

        private void Awake()
        {
            instance = this;

            foreach (Sound sound in sounds)
            {
                if (!sound.source)
                    sound.source = gameObject.AddComponent<AudioSource>();

                sound.source.clip = sound.clip;
                sound.source.playOnAwake = sound.playOnAwake;

                if (sound.playOnAwake)
                    sound.source.Play();

                sound.source.volume = sound.volume;
            }
        }

        public void Play(string name)
        {
            Sound sound = Array.Find(sounds, sound => sound.name == name);
            sound.source.Play();
        }

        public void Stop(string name)
        {
            Sound sound = Array.Find(sounds, sound => sound.name == name);
            sound.source.Stop();
        }
    }
}

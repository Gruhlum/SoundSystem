using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics;
using UnityEngine;
using UnityEngine.Audio;

namespace HexTecGames.SoundSystem
{    
    [System.Serializable]
    public class SoundArgsData
    {
        public float Volume
        {
            get
            {
                return volume;
            }
            set
            {
                volume = value;
            }
        }
        [Tooltip("The volume of the AudioSource")]
        private float volume = 1f;

        public float Pitch
        {
            get
            {
                return pitch;
            }
            set
            {
                pitch = value;
            }
        }
        [Tooltip("The pitch of the AudioSource")]
        private float pitch = 1f;

        public float Delay
        {
            get
            {
                return delay;
            }
            set
            {
                delay = value;
            }
        }
        [Tooltip("Extra wait time before the clip starts (in seconds)")]
        [SerializeField] private float delay = default;
        public float FadeIn
        {
            get
            {
                return fadeIn;
            }
            set
            {
                fadeIn = value;
            }
        }
        [Tooltip("Clip will fade in over X seconds")]
        [SerializeField] private float fadeIn = default;
        public float FadeOut
        {
            get
            {
                return fadeOut;
            }
            set
            {
                fadeOut = value;
            }
        }
        [Tooltip("Clip will fade out over X seconds")]
        [SerializeField] private float fadeOut = default;

        public float StartPosition
        {
            get
            {
                return startPosition;
            }
            set
            {
                startPosition = value;
            }
        }
        [Tooltip("Skips the first X seconds of the clip")]
        [SerializeField] private float startPosition = default;

        public bool Loop
        {
            get
            {
                return loop;
            }
            private set
            {
                loop = value;
            }
        }
        [Tooltip("Should the clip loop until manually stopped?")]
        [SerializeField] private bool loop = default;

        public AudioMixerGroup AudioMixerGroup { get; set; }

        public bool LimitInstances
        {
            get
            {
                return limitInstances;
            }
            private set
            {
                limitInstances = value;
            }
        }
        [Tooltip("Should there be a maximum amount playing at the same time of this specific clip?")]
        [SerializeField] private bool limitInstances;
        public LimitMode LimitMode
        {
            get
            {
                return limitMode;
            }
            private set
            {
                limitMode = value;
            }
        }
        [Tooltip(("Default: Uses mode set by the Sound Controller \n" +
            "Steal: Stops a currently playing sound \n" +
            "Prevent: New Sounds won't start"))]
        [SerializeField] private LimitMode limitMode = default;
        public int MaximumInstances
        {
            get
            {
                return maximumInstances;
            }
            private set
            {
                maximumInstances = value;
            }
        }
        [SerializeField, Min(1)] private int maximumInstances = 4;
    }
}
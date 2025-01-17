using HexTecGames.Basics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace HexTecGames.SoundSystem
{
    /// <summary>
    /// Contains various parameters for playing a sound.
    /// </summary>
    [CreateAssetMenu(fileName = "New Clip", menuName = "HexTecGames/SoundSystem/Clip", order = -1)]
    public class SoundClip : SoundClipBase
    {
        public AudioClip AudioClip
        {
            get
            {
                return audioClip;
            }
            private set
            {
                audioClip = value;
            }
        }
        [Tooltip("The AudioClip that will be played")]
        [SerializeField] private AudioClip audioClip = default;

        public FloatValue Volume
        {
            get
            {
                return volume;
            }
            private set
            {
                volume = value;
            }
        }
        [Tooltip("The volume of the AudioSource")]
        [SerializeField] private FloatValue volume = new FloatValue(0f, 1f);

        public FloatValue Pitch
        {
            get
            {
                return pitch;
            }
            private set
            {
                pitch = value;
            }
        }
        [Tooltip("The pitch of the AudioSource")]
        [SerializeField] private FloatValue pitch = new FloatValue(-3f, 3f);

        public float Delay
        {
            get
            {
                return delay;
            }
            private set
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
            private set
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
            private set
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
            private set
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
        [Tooltip(("Steal: Stops a currently playing sound \n" +
            "Prevent: New Sounds won't start"))]
        [SerializeField, DrawIf(nameof(limitInstances), true)] private LimitMode limitMode = default;
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
        [SerializeField, DrawIf(nameof(limitInstances), true), Min(1)] private int maximumInstances = 4;

        public AudioMixerGroup AudioMixerGroup
        {
            get
            {
                return this.audioMixerGroup;
            }
            private set
            {
                this.audioMixerGroup = value;
            }
        }
        [Tooltip("(Optional) The AudioMixerGroup that will be assigned to the AudioSource")]
        [SerializeField] private AudioMixerGroup audioMixerGroup;


        public override SoundSource Play()
        {
            SoundArgs args = new SoundArgs(this);
            return Play(args);
        }
        public override SoundSource Play(float volumeMulti = 1, float pitchMulti = 1)
        {
            SoundArgs args = new SoundArgs(this, volumeMulti, pitchMulti);
            return Play(args);
        }
        public override SoundSource Play(SoundArgs args)
        {
            args.Setup(this);
            SoundController.RequestTempSound(args);
            return args.source;
        }
        public SoundSource GetSoundSource()
        {
            SoundArgs args = new SoundArgs(this);
            SoundController.RequestPersistentSound(args);
            return args.source;
        }
        public override SoundClip GetSoundClip()
        {
            return this;
        }
    }
}
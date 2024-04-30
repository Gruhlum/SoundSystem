using HexTecGames.Basics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace HexTecGames.SoundSystem
{
    [CreateAssetMenu(fileName = "New Clip", menuName = "HexTecGames/SoundPack/Clip")]
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
        [SerializeField, DrawIf(nameof(limitInstances), true)] private LimitMode limitMode = default;
        public int MaximumInstances
        {
            get
            {
                return maxiumInstances;
            }
            private set
            {
                maxiumInstances = value;
            }
        }
        [SerializeField, DrawIf(nameof(limitInstances), true), Min(1)] private int maxiumInstances = 4;

        public AudioMixerGroup audioMixerGroup;

        public override void Play()
        {
            SoundArgs args = new SoundArgs(this);
            Play(args);
        }

        public override void Play(float volumeMulti = 1, float pitchMulti = 1)
        {
            SoundArgs args = new SoundArgs(this, volumeMulti, pitchMulti);
            Play(args);
        }

        public override void Play(SoundArgs args)
        {
            args.Setup(this);
            SoundController.RequestTempSound(args);
        }

        public override SoundClip GetSoundClip()
        {
            return this;
        }
    }
}
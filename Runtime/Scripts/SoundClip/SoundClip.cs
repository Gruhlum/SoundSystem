using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace HexTecGames.SoundSystem
{
    [CreateAssetMenu(fileName = "New Clip", menuName = "SoundPack/Clip")]
    public class SoundClip : SoundClipBase
    {
        public AudioClip AudioClip
        {
            get
            {
                return audioClip;
            }
            set
            {
                audioClip = value;
            }
        }
        [SerializeField] private AudioClip audioClip = default;
       
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
        [Range(0, 1)][SerializeField] private float volume = 0.5f;
    
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
        [Range(-3, 3)][SerializeField] private float pitch = 1f;


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
        [SerializeField] private float startPosition = default;

        public bool Loop
        {
            get
            {
                return loop;
            }
            set
            {
                loop = value;
            }
        }
        [SerializeField] private bool loop = default;
        public bool Unique
        {
            get
            {
                return unique;
            }
            set
            {
                unique = value;
            }
        }
        [SerializeField] private bool unique = default;


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
    }
}
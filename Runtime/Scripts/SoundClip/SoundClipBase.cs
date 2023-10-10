using HexTecGames.SoundSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace HexTecGames.SoundSystem
{
    public abstract class SoundClipBase : ScriptableObject
    {
        [Range(0, 1)]
        public float volume = 0.5f;
        [Range(-3, 3)]
        public float pitch = 1;

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

        public virtual void Play()
        {
            Play(new SoundArgs(this));
        }
        public virtual void Play(SoundArgs args)
        {
            SoundController.RequestTempSound(args);
        }
        public virtual void Play(float volumeMulti = 1f, float pitchMulti = 1f)
        {
            Play(new SoundArgs(this, volumeMulti, pitchMulti));
        }
        public static SoundClip GetNext(ReplayOrder order, ref int index, List<SoundClip> clips)
        {
            if (clips.Count == 2 && order == ReplayOrder.NonRepeating)
            {
                order = ReplayOrder.Order;
            }
            switch (order)
            {
                case ReplayOrder.Random:
                    return clips[Random.Range(0, clips.Count)];

                case ReplayOrder.NonRepeating:
                    if (clips.Count == 1)
                    {
                        return clips[0];
                    }
                    //TODO: Check if its working
                    int[] indexes = new int[clips.Count - 1];
                    int count = 0;
                    for (int i = 0; i < clips.Count; i++)
                    {
                        if (i != index)
                        {
                            indexes[count] = i;
                            count++;
                        }
                    }
                    index = indexes[Random.Range(0, indexes.Length)];
                    return clips[index];

                case ReplayOrder.Order:
                    if (clips.Count == 1)
                    {
                        return clips[0];
                    }
                    if (index > clips.Count)
                    {
                        index = 0;
                    }
                    else index++;
                    return clips[index];
                default:
                    return null;
            }
        }

        public abstract AudioClip GetAudioClip();
    }
}
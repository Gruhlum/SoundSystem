using HexTecGames.Basics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.SoundSystem
{
    public enum ReplayOrder { Random, NonRepeating, Order }

    [CreateAssetMenu(fileName = "New ClipGroup", menuName = "SoundPack/ClipGroup")]
    public class SoundClipGroup : SoundClipBase
    {
        public bool PlayAllAtOnce
        {
            get
            {
                return playAllAtOnce;
            }
            set
            {
                playAllAtOnce = value;
            }
        }
        [SerializeField] private bool playAllAtOnce = default;

        [DrawIf("playAllAtOnce", false)]
        public ReplayOrder Order;

        public List<SoundClip> SoundClips;

        [HideInInspector] public int lastIndex = -1;

        public override void Play()
        {
            if (PlayAllAtOnce)
            {
                foreach (var soundClip in SoundClips)
                {
                    soundClip.Play(volume, pitch);
                }
            }
            else base.Play();
        }
        public override void Play(SoundArgs args)
        {
            args.volMulti *= volume;
            args.pitchMulti *= pitch;
            if (PlayAllAtOnce)
            {
                foreach (var soundClip in SoundClips)
                {
                    soundClip.Play(args);
                }
            }
            else base.Play(args);
        }

        public override void Play(float volumeMulti = 1, float pitchMulti = 1)
        {
            if (PlayAllAtOnce)
            {
                foreach (var soundClip in SoundClips)
                {
                    soundClip.Play(volume * volumeMulti, pitch * pitchMulti);
                }
            }
            else base.Play(volumeMulti, pitchMulti);
        }

        public override void Play(float fadeIn = 0, float delay = 0, float volumeMulti = 1, float pitchMulti = 1, bool loop = false)
        {
            base.Play(fadeIn, delay, volumeMulti, pitchMulti, loop);
        }

        public override AudioClip GetAudioClip()
        {
            if (SoundClips == null || SoundClips.Count == 0)
            {
                return null;
            }
            return GetNext(Order, ref lastIndex, SoundClips).audioClip;
        }   
    }
}
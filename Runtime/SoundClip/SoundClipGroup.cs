using HexTecGames.Basics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.SoundSystem
{
    public enum ReplayOrder { Random, NonRepeating, Order }

    [CreateAssetMenu(fileName = "New ClipGroup", menuName = "HexTecGames/SoundSystem/ClipGroup")]
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

        private SoundClip lastClip = default;


        public override SoundSource Play()
        {
            return Play(new SoundArgs());
        }
        public override SoundSource Play(SoundArgs args)
        {
            if (PlayAllAtOnce)
            {
                foreach (var soundClip in SoundClips)
                {
                    args.Setup(soundClip);
                    soundClip.Play(args);
                }
                return null;
            }
            else
            {
                SoundClip clip = GetSoundClip();
                args.Setup(clip);
                return clip.Play(args);
            }
        }

        public override SoundSource Play(float volumeMulti = 1, float pitchMulti = 1)
        {
            if (PlayAllAtOnce)
            {
                foreach (var soundClip in SoundClips)
                {
                    soundClip.Play(volumeMulti, pitchMulti);
                }
                return null;
            }
            else
            {
                SoundClip clip = GetSoundClip();
                return clip.Play(volumeMulti, pitchMulti);
            }
        }

        private SoundClip RetrieveSoundClip()
        {
            if (SoundClips == null || SoundClips.Count == 0)
            {
                return null;
            }
            int index = 0;
            ReplayOrder order = Order;
            if (SoundClips.Count == 2 && order == ReplayOrder.NonRepeating)
            {
                order = ReplayOrder.Order;
            }
            switch (order)
            {
                case ReplayOrder.Random:
                    return SoundClips[Random.Range(0, SoundClips.Count)];

                case ReplayOrder.NonRepeating:
                    if (SoundClips.Count == 1)
                    {
                        return SoundClips[0];
                    }
                    //TODO: Check if its working
                    int[] indexes = new int[SoundClips.Count];
                    int count = 0;
                    for (int i = 0; i < SoundClips.Count; i++)
                    {
                        if (lastClip != SoundClips[i])
                        {
                            indexes[count] = i;
                            count++;
                        }
                    }
                    index = indexes[Random.Range(0, indexes.Length)];
                    return SoundClips[index];

                case ReplayOrder.Order:
                    if (SoundClips.Count == 1)
                    {
                        return SoundClips[0];
                    }
                    if (lastClip != null)
                    {
                        index = SoundClips.IndexOf(lastClip) + 1;
                    }

                    if (index >= SoundClips.Count)
                    {
                        index = 0;
                    }
                    return SoundClips[index];
                default:
                    return null;
            }
        }
        public override SoundClip GetSoundClip()
        {
            SoundClip clip = RetrieveSoundClip();
            lastClip = clip;
            return clip;
        }
    }

}
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
        [SerializeField] private ReplayOrder order;
        [SerializeField] private List<SoundClip> soundClips;
#if UNITY_EDITOR
        [SerializeField, TextArea] private string description = default;
#endif
        private SoundClip lastClip = default;


        public override SoundSource Play()
        {
            return Play(new SoundArgs());
        }
        public override SoundSource Play(SoundArgs args)
        {
            if (PlayAllAtOnce)
            {
                foreach (var soundClip in soundClips)
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
                foreach (var soundClip in soundClips)
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
            if (soundClips == null || soundClips.Count == 0)
            {
                return null;
            }
            int index = 0;
            ReplayOrder currentOrder = order;
            if (soundClips.Count == 2 && currentOrder == ReplayOrder.NonRepeating)
            {
                currentOrder = ReplayOrder.Order;
            }
            switch (currentOrder)
            {
                case ReplayOrder.Random:
                    return soundClips[Random.Range(0, soundClips.Count)];

                case ReplayOrder.NonRepeating:
                    if (soundClips.Count == 1)
                    {
                        return soundClips[0];
                    }
                    //TODO: Check if its working
                    int[] indexes = new int[soundClips.Count];
                    int count = 0;
                    for (int i = 0; i < soundClips.Count; i++)
                    {
                        if (lastClip != soundClips[i])
                        {
                            indexes[count] = i;
                            count++;
                        }
                    }
                    index = indexes[Random.Range(0, indexes.Length)];
                    return soundClips[index];

                case ReplayOrder.Order:
                    if (soundClips.Count == 1)
                    {
                        return soundClips[0];
                    }
                    if (lastClip != null)
                    {
                        index = soundClips.IndexOf(lastClip) + 1;
                    }

                    if (index >= soundClips.Count)
                    {
                        index = 0;
                    }
                    return soundClips[index];
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
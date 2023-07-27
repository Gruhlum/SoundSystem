using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HecTecGames.SoundSystem
{
    public enum ReplayOrder { Random, NonRepeating, Order }

    [CreateAssetMenu(fileName = "New ClipGroup", menuName = "SoundPack/ClipGroup")]
    public class SoundClipGroup : ScriptableObject
    {
        public ReplayOrder Order;

        public List<SoundClip> SoundClips;

        [HideInInspector] public int lastIndex = -1;

        public void Play(float fadeIn = 0, float delay = 0, float volMulti = 1, float pitchMulti = 1)
        {
            if (SoundClips == null || SoundClips.Count == 0)
            {
                return;
            }
            GetNext(Order).Play(fadeIn, delay, pitchMulti, volMulti);
        }
        public SoundClip GetNext(ReplayOrder order)
        {
            if (SoundClips.Count == 2)
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
                    int[] indexes = new int[SoundClips.Count - 1];
                    int count = 0;
                    for (int i = 0; i < SoundClips.Count; i++)
                    {
                        if (i != lastIndex)
                        {
                            indexes[count] = i;
                            count++;
                        }
                    }
                    lastIndex = indexes[Random.Range(0, indexes.Length)];
                    return SoundClips[lastIndex];

                case ReplayOrder.Order:
                    if (lastIndex >= SoundClips.Count - 1)
                    {
                        lastIndex = 0;
                    }
                    else lastIndex++;
                    return SoundClips[lastIndex];
                default:
                    return null;
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HexTecGames.SoundSystem
{
    [CreateAssetMenu(menuName = "HexTecGames/SoundPack/WeightedClipGroup")]
    public class WeightedClipGroup : SoundClipBase
    {
        public List<WeightedClip> SoundClips = new List<WeightedClip>();

        public override void Play()
        {
            GetSoundClip().Play();
        }

        public override void Play(SoundArgs args)
        {
            GetSoundClip().Play(args);
        }

        public override void Play(float volumeMulti = 1, float pitchMulti = 1)
        {
            GetSoundClip().Play(volumeMulti, pitchMulti);
        }

        public override SoundClip GetSoundClip()
        {
            if (SoundClips == null || SoundClips.Count == 0)
            {
                return null;
            }
            float totalValue = SoundClips.Sum(x => x.weight);
            for (int i = 0; i < SoundClips.Count; i++)
            {
                float rng = Random.Range(0, totalValue);
                if (rng < SoundClips[i].weight)
                {
                    return SoundClips[i].soundClip;
                }
                else totalValue -= SoundClips[i].weight;
            }
            Debug.Log("Shouldn't be here!");
            return SoundClips[0].soundClip;
        }
    }
}
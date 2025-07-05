using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HexTecGames.SoundSystem
{
    [CreateAssetMenu(menuName = "HexTecGames/SoundSystem/WeightedClipGroup", order = 2)]
    public class WeightedClipGroup : SoundClipBase
    {
        [SerializeField] private List<WeightedClip> soundClips = new List<WeightedClip>();
#if UNITY_EDITOR
        [SerializeField, TextArea] private string description = default;
#endif
        public override SoundSource Play()
        {
            return GetSoundClip().Play();
        }

        public override SoundSource Play(SoundArgs args)
        {
            return GetSoundClip().Play(args);
        }

        public override SoundSource Play(float volumeMulti = 1, float pitchMulti = 1)
        {
            return GetSoundClip().Play(volumeMulti, pitchMulti);
        }

        public override SoundClip GetSoundClip()
        {
            if (soundClips == null || soundClips.Count == 0)
            {
                return null;
            }
            float totalValue = soundClips.Sum(x => x.weight);
            for (int i = 0; i < soundClips.Count; i++)
            {
                float rng = Random.Range(0, totalValue);
                if (rng < soundClips[i].weight)
                {
                    return soundClips[i].soundClip;
                }
                else totalValue -= soundClips[i].weight;
            }
            Debug.Log("Shouldn't be here!");
            return soundClips[0].soundClip;
        }
    }
}
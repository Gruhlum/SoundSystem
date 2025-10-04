using System.Collections.Generic;
using HexTecGames.Basics;
using UnityEngine;

namespace HexTecGames.SoundSystem
{

    [CreateAssetMenu(fileName = "New ClipSoundGroup", menuName = "HexTecGames/SoundSystem/SoundClipGroup")]
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

        
        public SoundClip GetSoundClip()
        {
            SoundClip clip = soundClips.SelectByReplayOrder(lastClip, order);
            lastClip = clip;
            return clip;
        }

        public override SoundArgs GetSoundArgs()
        {
            return GetSoundClip().GetSoundArgs();
        }
    }

}
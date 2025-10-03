using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics;
using UnityEngine;

namespace HexTecGames.SoundSystem
{
    [CreateAssetMenu(fileName = "New AudioSoundGroup", menuName = "HexTecGames/SoundSystem/AudioClipGroup")]
    public class AudioClipGroup : SoundClipBase
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
        [SerializeField] private List<AudioClip> soundClips;
#if UNITY_EDITOR
        [SerializeField, TextArea] private string description = default;
#endif
        private AudioClip lastClip = default;


        public AudioClip GetAudioClip()
        {
            AudioClip clip = soundClips.SelectByReplayOrder(lastClip, order);
            lastClip = clip;
            return clip;
        }

        public override SoundArgs GetSoundArgs()
        {
            return new SoundArgs().WithClip(GetAudioClip());
        }
    }
}
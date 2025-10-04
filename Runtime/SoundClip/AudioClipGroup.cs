using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics;
using UnityEngine;

namespace HexTecGames.SoundSystem
{
    [CreateAssetMenu(fileName = "New AudioSoundGroup", menuName = "HexTecGames/SoundSystem/AudioClipGroup")]
    public class AudioClipGroup : SoundClipBase
    {
        [Tooltip("The volume of the AudioSource")]
        public FloatValue Volume
        {
            get
            {
                return volume;
            }
            private set
            {
                volume = value;
            }
        }
        [SerializeField] private FloatValue volume = new FloatValue(0f, 1f);

        public FloatValue Pitch
        {
            get
            {
                return pitch;
            }
            private set
            {
                pitch = value;
            }
        }
        [Tooltip("The pitch of the AudioSource")]
        [SerializeField] private FloatValue pitch = new FloatValue(-3f, 3f);
        public SoundArgsData Settings
        {
            get
            {
                return settings;
            }
            private set
            {
                settings = value;
            }
        }
        [SerializeField] private SoundArgsData settings = default;

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
            return new SoundArgs(Settings).WithClip(GetAudioClip()).WithVolume(Volume.Value).WithPitch(Pitch.Value);
        }
    }
}
using HexTecGames.Basics;
using UnityEngine;
using UnityEngine.Audio;

namespace HexTecGames.SoundSystem
{
    /// <summary>
    /// Contains various parameters for playing a sound.
    /// </summary>
    [CreateAssetMenu(fileName = "New Clip", menuName = "HexTecGames/SoundSystem/Clip", order = -1)]
    public class SoundClip : SoundClipBase
    {
        public AudioClip AudioClip
        {
            get
            {
                return audioClip;
            }
            private set
            {
                audioClip = value;
            }
        }
        [Tooltip("The AudioClip that will be played")]
        [SerializeField] private AudioClip audioClip = default;


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

        public AudioMixerGroup AudioMixerGroup
        {
            get
            {
                return this.audioMixerGroup;
            }
            private set
            {
                this.audioMixerGroup = value;
            }
        }

        [Tooltip("(Optional) The AudioMixerGroup that will be assigned to the AudioSource")]
        [SerializeField] private AudioMixerGroup audioMixerGroup;
#if UNITY_EDITOR
        [SerializeField, TextArea] private string description = default;
#endif

        [ContextMenu("Fix Slider Range")]
        public void FixFloatValues()
        {
            Volume.SetSliderRange(0, 1);
            Pitch.SetSliderRange(-3, 3);
        }

        public override SoundArgs GetSoundArgs()
        {
            return new SoundArgs(settings).WithVolume(Volume.Value).WithPitch(Pitch.Value);
        }
        public SoundClip GetSoundClip()
        {
            return this;
        }
    }
}
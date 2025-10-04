using System;
using UnityEngine;
using UnityEngine.Audio;

namespace HexTecGames.SoundSystem
{
    [System.Serializable]
    public class SoundArgs : EventArgs
    {
        public SoundArgsData data;
        public SoundClipBase soundClip;
        public AudioClip audioClip;

        public SoundSource source;
        public bool failed;


        public SoundArgs(SoundClipBase soundClip, SoundArgsData data)
        {
            this.soundClip = soundClip;
            this.data = data;
        }
        public SoundArgs(SoundClipBase soundClip)
        {
            this.soundClip = soundClip;
            this.data = new SoundArgsData();
        }
        public SoundArgs WithClip(AudioClip clip)
        {
            this.audioClip = clip;
            return this;
        }

        public SoundArgs WithMixer(AudioMixerGroup mixer)
        {
            this.data.AudioMixerGroup = mixer;
            return this;
        }

        public SoundArgs WithVolume(float volume)
        {
            this.data.Volume = volume;
            return this;
        }

        public SoundArgs WithPitch(float pitch)
        {
            this.data.Pitch = pitch;
            return this;
        }
    }
}
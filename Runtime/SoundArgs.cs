using System;
using UnityEngine;
using UnityEngine.Audio;

namespace HexTecGames.SoundSystem
{
    [System.Serializable]
    public class SoundArgs : EventArgs
    {
        public IHasMaximumInstances hasMaximumInstances;
        public SoundArgsData data;
        public AudioClip audioClip;
        public AudioMixerGroup audioMixerGroup;
        public bool limitInstances;
        public int maximumInstances;
        public LimitMode limitMode;
        public bool loop;
        public SoundSource source;
        public bool failed;


        public SoundArgs()
        { }
        public SoundArgs(IHasMaximumInstances hasMaximumInstances)
        {
            this.hasMaximumInstances = hasMaximumInstances;
        }
        public SoundArgs(SoundArgsData data)
        {
            this.data = data;
        }
        public SoundArgs WithClip(AudioClip clip)
        {
            this.audioClip = clip;
            return this;
        }

        public SoundArgs WithMixer(AudioMixerGroup mixer)
        {
            this.audioMixerGroup = mixer;
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
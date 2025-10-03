using System;
using UnityEngine;
using UnityEngine.Audio;

namespace HexTecGames.SoundSystem
{
    [System.Serializable]
    public class SoundArgs : EventArgs
    {
        public IHasMaximumInstances hasMaximumInstances;
        public AudioClip audioClip;
        public AudioMixerGroup audioMixerGroup;
        public float fadeIn;
        public float fadeOut;
        public float delay;
        public bool limitInstances;
        public int maximumInstances;
        public LimitMode limitMode;
        public bool loop;
        public float volumeMulti = 1;
        public float pitchMulti = 1;
        public float startPosition;
        public SoundSource source;
        public bool failed;


        public SoundArgs()
        { }
        public SoundArgs(IHasMaximumInstances hasMaximumInstances)
        {
            this.hasMaximumInstances = hasMaximumInstances;
        }
        public SoundArgs(SoundClip clip)
        {
            this.hasMaximumInstances = clip;
            this.volumeMulti = clip.Volume.Value;
            this.pitchMulti = clip.Pitch.Value;
            this.fadeIn = clip.FadeIn;
            this.fadeOut = clip.FadeOut;
            this.delay = clip.Delay;
            this.startPosition = clip.StartPosition;
            this.loop = clip.Loop;
            this.limitInstances = clip.LimitInstances;
            this.maximumInstances = clip.MaximumInstances;
            this.limitMode = clip.LimitMode;
            this.audioClip = clip.AudioClip;
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
            this.volumeMulti = volume;
            return this;
        }

        public SoundArgs WithPitch(float pitch)
        {
            this.pitchMulti = pitch;
            return this;
        }
    }
}
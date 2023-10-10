using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.SoundSystem
{
    public class SoundArgs : EventArgs
    {
        //SoundClip clip, float delay, float fadeIn, float volMulti, float pitchMulti, bool loop

        public SoundClipBase soundClip;
        public AudioClip audioClip;
        public float fadeIn;
        public float fadeOut;
        public float delay;
        public bool unique;
        public bool loop;
        public float volumeMulti = 1;
        public float pitchMulti = 1;
        public float startPosition;
        public SoundSource source;
        public bool failed;

        public SoundArgs(SoundClipBase clip) : this(clip, clip.GetAudioClip()) { }

        public SoundArgs(SoundClipBase clip, float volumeMulti = 1, float pitchMulti = 1) : this(clip)
        {
            this.volumeMulti = volumeMulti;
            this.pitchMulti = pitchMulti;
        }

        public SoundArgs(SoundClipBase clip, AudioClip audioClip)
        {
            this.soundClip = clip;
            this.audioClip = audioClip;
            this.fadeIn = clip.FadeIn;
            this.fadeOut = clip.FadeOut;
            this.delay = clip.Delay;
            this.startPosition = clip.StartPosition;
            this.loop = clip.Loop;
            this.unique = clip.Unique;
        }
        public SoundArgs(SoundClipBase clip, AudioClip audioClip, float volumeMulti = 1, float pitchMulti = 1) : this(clip, audioClip)
        {
            this.volumeMulti = volumeMulti;
            this.pitchMulti = pitchMulti;
        }
    }
}
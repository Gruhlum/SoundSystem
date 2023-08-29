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
        public float delay;
        public bool loop;
        public float volMulti;
        public float pitchMulti;
        public SoundSource source;
        public bool failed;

        public SoundArgs(SoundClipBase clip, AudioClip audioClip, float fadeIn = 0, float delay = 0, float volMulti = 1, float pitchMulti = 1, bool loop = false)
        {
            this.soundClip = clip;
            this.audioClip = audioClip;
            this.fadeIn = fadeIn;
            this.delay = delay;
            this.volMulti = volMulti;
            this.pitchMulti = pitchMulti;
            this.loop = loop;
        }
        public SoundArgs(SoundClipBase clip, float fadeIn = 0, float delay = 0, float volMulti = 1, float pitchMulti = 1, bool loop = false)
        {
            this.soundClip = clip;
            this.fadeIn = fadeIn;
            this.delay = delay;
            this.volMulti = volMulti;
            this.pitchMulti = pitchMulti;
            this.loop = loop;
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HecTecGames.SoundSystem
{
    public class SoundArgs : EventArgs
    {
        //SoundClip clip, float delay, float fadeIn, float volMulti, float pitchMulti, bool loop

        public SoundClip clip;
        public float fadeIn;
        public float delay;
        public float volMulti;
        public float pitchMulti;
        public bool loop;
        public SoundSource source;
        public bool failed;

        public SoundArgs(SoundClip clip, float fadeIn = 0, float delay = 0, float volMulti = 1, float pitchMulti = 1, bool loop = false)
        {
            this.clip = clip;
            this.fadeIn = fadeIn;
            this.delay = delay;
            this.volMulti = volMulti;
            this.pitchMulti = pitchMulti;
            this.loop = loop;
        }
    }
}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.SoundSystem
{
    [System.Serializable]
    public class PersistentSound
    {
		public SoundClip soundClip;
        public SoundSource Source
        {
            get
            {
                return source;
            }
            set
            {
                source = value;
            }
        }
        private SoundSource source;

        public void Play(float volumeMulti = 1f, float pitchMulti = 1f)
        {
            if (Source != null)
            {
                if (Source.IsActive)
                {
                    return;
                }
            }
            SoundArgs args = new SoundArgs(soundClip, volumeMulti, pitchMulti);
            soundClip.Play(args);
            this.Source = args.source;
        }
        public void Stop(float delay = 0, float fadeOut = 0)
        {
            if (Source == null)
            {
                return;
            }
            Source.Stop(delay, fadeOut);
        }
    }
}
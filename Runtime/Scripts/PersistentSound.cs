using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HecTecGames.SoundSystem
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

        [SerializeField] private bool loop = default;

        public void Play(float delay = 0, float fadeIn = 0f, float volumeMulti = 1f, float pitchMulti = 1f)
        {
            if (Source != null)
            {
                if (Source.IsActive)
                {
                    return;
                }
            }
            SoundArgs args = new SoundArgs(soundClip, fadeIn, delay, volumeMulti, pitchMulti, loop);
            SoundController.RequestTempSound(args);
            this.Source = args.source;
        }
        public void Stop(float delay = 0, float fadeOut = 0)
        {
            if (Source == null)
            {
                return;
            }
            Source.StopSound(delay, fadeOut);
        }
    }
}
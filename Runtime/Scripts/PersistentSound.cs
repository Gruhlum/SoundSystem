using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HecTecGames.SoundSystem
{
	[System.Serializable]
    public class PersistentSound
	{
		public SoundClip soundClip;

		private SoundSource soundSource;

        [SerializeField] private bool loop = default;

        private void GetSoundSource()
        {
            soundSource = SoundController.GetSoundSource();
            soundSource.Setup(soundClip);
            soundSource.Loop = loop;
            soundSource.name = soundClip.name;
            soundSource.Volume = 0;
        }

        public void FadeOut(float time)
        {
            if (soundSource == null)
            {
                return;
            }
            soundSource.StartFadeOut(time);
        }
        public void FadeIn(float time)
        {
            if (soundSource == null)
            {
                GetSoundSource();
            }
            soundSource.StartFadeIn(time);
        }

		public void PlaySound()
        {
            if (soundSource == null)
            {
                GetSoundSource();
            }
            if (!soundSource.IsPlaying)
            {
                soundSource.PlaySound();
            }			
        }
		public void StopSound()
        {
            if (soundSource == null || soundSource.IsPlaying == false)
            {
                return;
            }
            else soundSource.StopSound();
        }
	}
}
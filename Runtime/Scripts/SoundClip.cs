using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace HecTecGames.SoundSystem
{
	[CreateAssetMenu(fileName = "New Clip", menuName = "SoundPack/Clip")]
	public class SoundClip : ScriptableObject
	{
		public AudioClip audioClip;
        [Range(0, 1)]
        public float volume = 0.5f;
		[Range(-3, 3)]
		public float pitch = 1;
		public AudioMixerGroup audioMixerGroup;       

        public SoundArgs Play(float fadeIn = 0f, float delay = 0, float volumeMulti = 1f, float pitchMulti = 1f, bool loop = false)
        {
			SoundArgs args = new SoundArgs(this, fadeIn, delay, volumeMulti, pitchMulti, loop);
			SoundController.RequestTempSound(args);
			return args;
		}
		public void Play()
        {
			SoundController.RequestTempSound(new SoundArgs(this));
		}
		//public SoundArgs Play(SoundArgs args)
  //      {
		//	SoundController.RequestTempSound(args);
		//	return args;
		//}
	}
}
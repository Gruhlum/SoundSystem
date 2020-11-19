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

		public void PlaySound()
        {
			SoundController.RequestTempSound(this);
        }
	}
}
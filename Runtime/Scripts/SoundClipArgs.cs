using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HecTecGames.SoundSystem
{
	public class SoundClipArgs
	{
		public float? Volume;
		public float? Pitch;

		public SoundClipArgs(float volume, float pitch)
        {
			Volume = volume;
			Pitch = pitch;
        }
	}
}
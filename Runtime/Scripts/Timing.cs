using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames
{
	[System.Serializable]
	public class Timing
	{
		public float initDelay;
		public float cooldown;
		private float timer;

		public void ResetTimer()
		{
			timer = 0;
			timer -= initDelay;
        }

		public bool AdvanceTime(float time)
		{
			timer += time;
			if (timer >= cooldown)
			{
				timer -= cooldown;
				return true;
			}
			return false;
		}
	}
}
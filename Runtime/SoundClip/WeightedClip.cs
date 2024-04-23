using HexTecGames.SoundSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames
{
	[System.Serializable]
	public class WeightedClip
	{
		public SoundClip soundClip;
		public float weight;
	}
}
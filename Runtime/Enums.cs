using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.SoundSystem
{
	public enum LimitMode { Default, Steal, Prevent }
	public enum ValueMode { Flat, Random }
	[Flags]
	public enum UnityEventType {None = 0, Start = 1, Destroy = 2, Enable = 4, Disable = 8 }
}
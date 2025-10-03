using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.SoundSystem
{
    public interface IHasMaximumInstances
    {
        public LimitMode LimitMode { get; }
        public int MaximumInstances { get; }

    }
}
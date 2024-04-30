using HexTecGames.SoundSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace HexTecGames.SoundSystem
{
    public abstract class SoundClipBase : ScriptableObject
    {
        public abstract void Play();
        public abstract void Play(SoundArgs args);
        public abstract void Play(float volumeMulti = 1f, float pitchMulti = 1f);

        public abstract SoundClip GetSoundClip();
    }
}
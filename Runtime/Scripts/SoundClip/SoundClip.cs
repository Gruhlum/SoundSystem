using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace HexTecGames.SoundSystem
{
    [CreateAssetMenu(fileName = "New Clip", menuName = "SoundPack/Clip")]
    public class SoundClip : SoundClipBase
    {
        public AudioClip audioClip;

        public override AudioClip GetAudioClip()
        {
            return audioClip;
        }

        //public SoundArgs Play(SoundArgs args)
        //      {
        //	SoundController.RequestTempSound(args);
        //	return args;
        //}
    }
}
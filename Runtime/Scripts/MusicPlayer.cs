using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.SoundSystem
{
    [System.Serializable]
    public class MusicPlayer
    {
        public List<SoundClip> MusicClips = new List<SoundClip>();

        public ReplayOrder Order;


        private static int index = 0;

        private static float duration;

        public bool AdvanceTime(float time)
        {
            duration -= time;
            if (duration <= 0)
            {
                return true;
            }
            return false;
        }
        public void SetDuration(float value)
        {
            duration = value;
        }

        public SoundArgs GetNext()
        {
            if (MusicClips == null || MusicClips.Count == 0)
            {
                return null;
            }

            SoundClip nextClip = SoundClip.GetNext(Order, ref index, MusicClips);
            duration = nextClip.audioClip.length;
            return new SoundArgs(nextClip);
        }
    }
}
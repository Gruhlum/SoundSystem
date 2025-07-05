using UnityEngine;

namespace HexTecGames.SoundSystem
{
    [System.Serializable]
    public class MusicPlayer : ClipPlayer
    {
        private static new SoundSource source;
        private static float duration;

        private void Start()
        {
            if (source == null)
            {
                PlayNextSong();
            }
        }

        private void Update()
        {
            if (source == null)
            {
                return;
            }
            duration -= Time.deltaTime;
            if (duration <= 0)
            {
                PlayNextSong();
            }
        }

        public void PlayNextSong()
        {
            SoundArgs args = new SoundArgs();
            soundClip.Play(args);
            if (args == null)
            {
                return;
            }
            duration = args.audioClip.length;
            source = args.source;
        }

        public void SetDuration(float value)
        {
            duration = value;
        }
    }
}
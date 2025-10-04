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
            source = soundClip.Play();
            duration = source.AudioSource.clip.length;
        }

        public void SetDuration(float value)
        {
            duration = value;
        }
    }
}
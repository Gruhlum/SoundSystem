using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.SoundSystem
{
	public class SoundBoard : MonoBehaviour
	{
        public GameObject MusicGO
        {
            get
            {
                return musicGO;
            }
            set
            {
                musicGO = value;
            }
        }
        [SerializeField] private GameObject musicGO = default;

        public GameObject SoundGO
        {
            get
            {
                return soundGO;
            }
            set
            {
                soundGO = value;
            }
        }
        [SerializeField] private GameObject soundGO = default;

        public float GetMusicDuration()
        {
            SoundSource source = MusicGO.GetComponentInChildren<SoundSource>();
            if (source != null)
            {
                return source.AudioSource.clip.length - source.AudioSource.time;
            }
            return -1;
        }

    }
}
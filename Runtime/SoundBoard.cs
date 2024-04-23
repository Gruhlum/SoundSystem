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
        private GameObject musicGO = default;

        public GameObject TempSoundGO
        {
            get
            {
                return tempSoundGO;
            }
            set
            {
                tempSoundGO = value;
            }
        }
        private GameObject tempSoundGO = default;

        public GameObject PersistentSoundGO
        {
            get
            {
                return persistentSoundGO;
            }
            set
            {
                persistentSoundGO = value;
            }
        }
        private GameObject persistentSoundGO = default;

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
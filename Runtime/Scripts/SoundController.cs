using HexTecGames.Basics;
using System;
using System.Linq;
using UnityEngine;

namespace HecTecGames.SoundSystem
{
	public class SoundController : MonoBehaviour
	{
        [SerializeField] private SoundSource soundSourcePrefab = default;

        private Spawner<SoundSource> sourceSpawner = new Spawner<SoundSource>();

        private static event Action<SoundClip> TempSoundRequested;
        private delegate SoundSource GeetSoundSource();
        private static event GeetSoundSource SoundSourceRequested;

        private SoundBoard soundBoard;

        private void Awake()
        {
            SetSoundBoard();          
            sourceSpawner.Prefab = soundSourcePrefab;
            sourceSpawner.Parent = soundBoard.transform.Find("TempSounds");
            TempSoundRequested += PlayTempSound;
            SoundSourceRequested += SoundController_SoundSourceRequested;
        }

        private SoundSource SoundController_SoundSourceRequested()
        {
            return Instantiate(soundSourcePrefab, this.transform);
        }

        private void SetSoundBoard()
        {
            soundBoard = FindObjectOfType<SoundBoard>();
            if (soundBoard == null)
            {
                soundBoard = new GameObject("SoundBoard").AddComponent<SoundBoard>();
                DontDestroyOnLoad(soundBoard);
                GameObject child = new GameObject("TempSounds");
                child.transform.SetParent(soundBoard.transform);
                child = new GameObject("Music");
                child.transform.SetParent(soundBoard.transform);
            }
            else
            {
                sourceSpawner.ConsumeInstances(soundBoard.transform.Find("TempSounds").GetComponentsInChildren<SoundSource>().ToList());
            } 
        }

        private void PlayTempSound(SoundClip clip)
        {
            sourceSpawner.Spawn().Setup(clip);
        }

        public static SoundSource GetSoundSource()
        {
            if (SoundSourceRequested != null)
            {
                return SoundSourceRequested.Invoke();
            }
            else
            {
                Debug.LogWarning("No SoundController active in scene!");
                return null;
            } 
        }

        public static void RequestTempSound(SoundClip clip)
        {
            if (TempSoundRequested != null)
            {
                TempSoundRequested.Invoke(clip);
            }
            else Debug.LogWarning("No SoundController active in scene!");
        }
    }
}
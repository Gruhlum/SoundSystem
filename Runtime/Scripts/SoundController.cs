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

        private static event Action<SoundArgs> TempSoundRequested;

        private SoundBoard soundBoard;

        private void Awake()
        {
            SetSoundBoard();
            sourceSpawner.Prefab = soundSourcePrefab;
            sourceSpawner.Parent = soundBoard.transform.Find("TempSounds");
            TempSoundRequested += PlayTempSound;
        }

        private void OnDisable()
        {
            TempSoundRequested -= PlayTempSound;
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
                sourceSpawner.AddInstances(soundBoard.transform.Find("TempSounds").GetComponentsInChildren<SoundSource>().ToList());
            }
        }

        private void PlayTempSound(SoundArgs args)
        {
            SoundSource source = sourceSpawner.Spawn();
            source.Setup(args.clip);
            source.PlaySound(args.delay, args.fadeIn, args.volMulti, args.pitchMulti, args.loop);
            args.source = source;
        }

        public static void RequestTempSound(SoundArgs args)
        {
            if (TempSoundRequested != null)
            {
                TempSoundRequested.Invoke(args);
                return;
            }
            Debug.LogWarning("No SoundController active in scene!");
            args.failed = true;
        }
    }
}
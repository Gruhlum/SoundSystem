using HexTecGames.Basics;
using HexTecGames.SoundSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HexTecGames.SoundSystem
{
    public class SoundController : MonoBehaviour
    {
        [SerializeField] private SoundSource soundSourcePrefab = default;

        private SpawnableSpawner<SoundSource> sourceSpawner = new SpawnableSpawner<SoundSource>();

        private static event Action<SoundArgs> OnTempSoundRequested;
        private static event Action<SoundArgs> OnPersistentSoundRequested;

        private SoundBoard soundBoard;

        private Dictionary<SoundClip, List<SoundSource>> activeSources = new Dictionary<SoundClip, List<SoundSource>>();

        private void Awake()
        {
            SetSoundBoard();
            if (soundSourcePrefab == null)
            {
                GameObject go = new GameObject("SoundSourcePrefab");
                SoundSource source = go.AddComponent<SoundSource>();
                soundSourcePrefab = source;
                go.transform.SetParent(soundBoard.transform);
            }
            sourceSpawner.Prefab = soundSourcePrefab;
            sourceSpawner.Parent = soundBoard.transform.Find("TempSounds");
            OnTempSoundRequested += TemporarySoundRequested;
            OnPersistentSoundRequested += PersistentSoundRequested;
        }

        private void OnDisable()
        {
            OnTempSoundRequested -= TemporarySoundRequested;
            OnPersistentSoundRequested -= PersistentSoundRequested;
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
                soundBoard.TempSoundGO = child;

                child = new GameObject("PersistentSounds");
                child.transform.SetParent(soundBoard.transform);
                soundBoard.PersistentSoundGO = child;

                child = new GameObject("Music");
                child.transform.SetParent(soundBoard.transform);
                soundBoard.MusicGO = child;
            }
            else
            {
                sourceSpawner.AddInstances(soundBoard.transform.Find("TempSounds").GetComponentsInChildren<SoundSource>().ToHashSet());
            }
        }

        private void TemporarySoundRequested(SoundArgs args)
        {
            if (args.audioClip == null)
            {
                return;
            }
            activeSources.TryGetValue(args.soundClip, out List<SoundSource> sources);

            if (PreventPlayForInstanceLimiting(args, sources))
            {
                return;
            }

            SoundSource source = sourceSpawner.Spawn();
            source.OnDeactivated += Source_OnDeactivated;

            if (sources != null)
            {
                sources.Add(source);
            }
            else activeSources.Add(args.soundClip, new List<SoundSource>() { source });

            source.Play(args);
        }

        /// <summary>
        /// Checks and applies instance limiting and returns whether the clip should be prevented to play.
        /// </summary>
        /// <param name="args"></param>
        /// <param name="sources"></param>
        /// <returns>Returns true if the play should be prevented.</returns>
        private bool PreventPlayForInstanceLimiting(SoundArgs args, List<SoundSource> sources)
        {
            if (!args.limitInstances)
            {
                return false;
            }
            if (sources == null)
            {
                return false;
            }
            if (args.maximumInstances <= 0)
            {
                return true;
            }
            if (sources.Count < args.maximumInstances)
            {
                return false;
            }
            if (args.limitMode == LimitMode.Prevent)
            {
                return true;
            }
            SoundSource oldSource = sources[0];
            oldSource.Stop();
            return false;
        }

        private void PersistentSoundRequested(SoundArgs args)
        {
            var source = Instantiate(sourceSpawner.Prefab);

            source.Play(args);
            source.transform.SetParent(soundBoard.PersistentSoundGO.transform);
        }

        private void Source_OnDeactivated(ISpawnable source)
        {
            source.OnDeactivated -= Source_OnDeactivated;

            if (source is not SoundSource soundSource)
            {
                return;
            }
            if (activeSources.TryGetValue(soundSource.SoundClip, out List<SoundSource> sources))
            {
                sources.Remove(soundSource);
            }
            else Debug.Log("Missing SoundSource, this shouldn't happen!");
        }
        private static SoundController CreateSoundController()
        {
            GameObject go = new GameObject("SoundController");
            return go.AddComponent<SoundController>();
        }
        public static void RequestPersistentSound(SoundArgs args)
        {
            if (OnPersistentSoundRequested == null)
            {
                CreateSoundController().PersistentSoundRequested(args);
                return;
            }
            else OnPersistentSoundRequested.Invoke(args);
        }
        public static void RequestTempSound(SoundArgs args)
        {
            if (OnTempSoundRequested == null)
            {
                CreateSoundController().TemporarySoundRequested(args);
                return;
            }
            else OnTempSoundRequested.Invoke(args);
        }
    }
}
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

        private Spawner<SoundSource> sourceSpawner = new Spawner<SoundSource>();

        private static event Action<SoundArgs> OnTempSoundRequested;
        private static event Action<SoundArgs> OnPersistentSoundRequested;

        private SoundBoard soundBoard;

        private Dictionary<SoundClip, List<SoundSource>> activeSources = new Dictionary<SoundClip, List<SoundSource>>();

        private void Awake()
        {
            SetSoundBoard();

            sourceSpawner.Prefab = soundSourcePrefab;
            sourceSpawner.Parent = soundBoard.transform.Find("TempSounds");
            OnTempSoundRequested += TemporarySoundRequested;
            OnPersistentSoundRequested += PersistentSoundRequested;
        }
      
        private void OnDisable()
        {
            OnTempSoundRequested -= TemporarySoundRequested;
        }

        //private void Update()
        //{
        //    if (musicPlayer.AdvanceTime(Time.deltaTime))
        //    {
        //        PlayMusic(musicPlayer.GetNext());
        //    }
        //}

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
                sourceSpawner.AddInstances(soundBoard.transform.Find("TempSounds").GetComponentsInChildren<SoundSource>().ToList());
            }
        }

        private void TemporarySoundRequested(SoundArgs args)
        {
            if (args.audioClip == null)
            {
                return;
            }
            activeSources.TryGetValue(args.soundClip, out List<SoundSource> sources);
            if (args.limitInstances)
            {
                if (args.maximumInstances <= 0)
                {
                    return;
                }
                if (sources != null && sources.Count >= args.maximumInstances)
                {
                    if (args.limitMode == LimitMode.Default || args.limitMode == LimitMode.Steal)
                    {                        
                        SoundSource oldSource = sources[0];
                        oldSource.Stop();
                    }
                    else return;
                }              
            }
            SoundSource source = sourceSpawner.Spawn();
            source.OnFinishedPlaying += Source_OnFinishedPlaying;
            if (sources != null)
            {
                sources.Add(source);
            }
            else activeSources.Add(args.soundClip, new List<SoundSource>());
            source.Play(args);
        }
        private void PersistentSoundRequested(SoundArgs args)
        {
            var source = Instantiate(sourceSpawner.Prefab);
           
            source.Play(args);
            source.transform.SetParent(soundBoard.PersistentSoundGO.transform);
        }

        private void Source_OnFinishedPlaying(SoundSource source)
        {
            if (activeSources.TryGetValue(source.SoundClip, out List<SoundSource> sources))
            {
                sources.Remove(source);
            }
            else Debug.Log("Missing SoundSource, this shouldn't happen!");
            source.OnFinishedPlaying -= Source_OnFinishedPlaying;
        }

        public static void RequestPersistentSound(SoundArgs args)
        {
            if (OnPersistentSoundRequested == null)
            {
                Debug.LogWarning("No SoundController active in scene!");
                args.failed = true;
            }
            else OnPersistentSoundRequested.Invoke(args);
        }
        public static void RequestTempSound(SoundArgs args)
        {
            if (OnTempSoundRequested == null)
            {
                Debug.LogWarning("No SoundController active in scene!");
                args.failed = true;
            }
            else OnTempSoundRequested.Invoke(args);
        }

        //public void ChangeGlobalVolume(float value)
        //{
        //    if (!Mathf.Approximately(value, currentVol))
        //    {
        //        lastVol = currentVol;
        //    }

        //    masterMixer.SetFloat(volumParam, Mathf.Log(value) * 20);
        //    currentVol = value;
        //}

        //public void ToggleMute()
        //{
        //    if (currentVol > 0.001f)
        //    {
        //        currentVol = 0.001f;
        //        masterMixer.SetFloat(volumParam, 0f);
        //    }
        //    else currentVol = lastVol;

        //    globalVolumeSlider.value = currentVol;
        //}
    }
}
using HexTecGames.Basics;
using HexTecGames.SoundSystem;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HexTecGames.SoundSystem
{
    public class SoundController : MonoBehaviour
    {
        private string volumParam = "volume";
        [SerializeField] private SoundSource soundSourcePrefab = default;

        private Spawner<SoundSource> sourceSpawner = new Spawner<SoundSource>();

        private static event Action<SoundArgs> TempSoundRequested;

        private SoundBoard soundBoard;

        [SerializeField] private Slider globalVolumeSlider = default;

        private static float lastVol;
        private static float currentVol = 0.5f;

        [SerializeField] private MusicPlayer musicPlayer = default;

        [SerializeField] private AudioMixer masterMixer = default;



        private void Awake()
        {
            SetSoundBoard();

            sourceSpawner.Prefab = soundSourcePrefab;
            sourceSpawner.Parent = soundBoard.transform.Find("TempSounds");
            TempSoundRequested += PlayTempSound;
        }
        private void Start()
        {
            if (globalVolumeSlider != null)
            {
                globalVolumeSlider.value = currentVol;
            }
            string setting = SaveSystem.LoadSettings(volumParam);
            if (string.IsNullOrEmpty(setting))
            {
                currentVol = Convert.ToInt16(setting);
            }
            if (masterMixer != null)
            {
                masterMixer.SetFloat(volumParam, Mathf.Log(currentVol) * 20);
                masterMixer.GetFloat(volumParam, out float val);
            }          

            float value = soundBoard.GetMusicDuration();
            if (value <= 0)
            {
                PlayMusic(musicPlayer.GetNext());
            }
            else musicPlayer.SetDuration(value);
        }
        private void OnDisable()
        {
            TempSoundRequested -= PlayTempSound;
            SaveSystem.SaveSettings(volumParam, currentVol.ToString());
        }

        private void FixedUpdate()
        {
            if (musicPlayer.AdvanceTime(Time.deltaTime))
            {
                PlayMusic(musicPlayer.GetNext());
            }
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
                soundBoard.SoundGO = child;

                child = new GameObject("Music");
                child.transform.SetParent(soundBoard.transform);
                soundBoard.MusicGO = child;
            }
            else
            {
                sourceSpawner.AddInstances(soundBoard.transform.Find("TempSounds").GetComponentsInChildren<SoundSource>().ToList());
            }
        }

        private void PlayTempSound(SoundArgs args)
        {
            SoundSource source = sourceSpawner.Spawn();
            source.PlaySound(args);
        }
        private void PlayMusic(SoundArgs args)
        {
            if (args == null)
            {
                return;
            }
            SoundSource source = sourceSpawner.Spawn();
            source.transform.SetParent(soundBoard.MusicGO.transform);
            source.PlaySound(args);
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

        public void ChangeGlobalVolume(float value)
        {
            if (!Mathf.Approximately(value, currentVol))
            {
                lastVol = currentVol;
            }

            masterMixer.SetFloat(volumParam, Mathf.Log(value) * 20);
            currentVol = value;
        }

        public void ToggleMute()
        {
            if (currentVol > 0f)
            {
                currentVol = 0;
                masterMixer.SetFloat(volumParam, 0f);
            }
            else currentVol = lastVol;

            globalVolumeSlider.value = currentVol;
        }
    }
}
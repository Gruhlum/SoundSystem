using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using HexTecGames.SoundSystem;

namespace HexTecGames._SoundExample
{    
    public class SoundTestController : AdvancedBehaviour
    {
        [SerializeField] private TMP_Text nameGUI = default;
        [SerializeField] private TMP_Text volumeGUI = default;
        [SerializeField] private TMP_Text pitchGUI = default;

        private SoundSource lastSoundSource;

        private void Awake()
        {
            nameGUI.text = string.Empty;
            volumeGUI.text = string.Empty;
            pitchGUI.text = string.Empty;
        }

        private void Start()
        {
            var results = FindObjectsByType<TestButton>(FindObjectsSortMode.None);
            foreach (var result in results)
            {
                result.PlayStarting += Result_PlayStarting;
            }
        }

        private void Update()
        {
            if (lastSoundSource != null)
            {
                UpdateValues(lastSoundSource);
            }
        }

        private void Result_PlayStarting(TestButton btn)
        {
            lastSoundSource = btn.SoundSource;
            nameGUI.text = lastSoundSource.Args.audioClip.name;
            UpdateValues(lastSoundSource);
        }

        private void UpdateValues(SoundSource soundSource)
        {
            if (soundSource == null)
            {
                return;
            }
            volumeGUI.text = soundSource.AudioSource.volume.ToString("#0.00");
            pitchGUI.text = soundSource.AudioSource.pitch.ToString("#0.00");
        }
    }
}
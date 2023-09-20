using HexTecGames.Basics;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace HexTecGames.SoundSystem
{
	public class SoundSettingsController : SoundControl
    {
        [SerializeField] private List<AudioSlider> sliders = default;
        

        private void Start()
        {
            LoadSettings();
        }

        public void SaveSettings()
        {
            foreach (var slider in sliders)
            {
                SaveVolume(slider);
            }
        }
        private void LoadSettings()
        {
            foreach (var slider in sliders)
            {
                LoadVolume(slider);
            }
        }    

        public void OnSliderChanged(AudioSlider slider, float value)
        {
            ChangeVolume(slider.AudioParam, value);
        }
    }
}
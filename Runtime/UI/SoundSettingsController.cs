using HexTecGames.Basics;
using System;
using System.Collections;
using System.Collections.Generic;
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
            LoadVolume();
        }

        public override void SaveVolume()
        {
            foreach (var slider in sliders)
            {
                SaveVolume(slider);
            }
        }
        protected override void LoadVolume()
        {
            foreach (var slider in sliders)
            {
                LoadVolume(slider);
            }
        }    

        public void OnSliderChanged(AudioSlider slider, float value)
        {
            ChangeVolume(slider);
        }
    }
}
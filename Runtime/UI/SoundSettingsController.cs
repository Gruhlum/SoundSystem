using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.SoundSystem
{
    public class SoundSettingsController : SoundControl
    {
        [SerializeField] private List<AudioSlider> sliders = default;

        public override void SaveVolume()
        {
            foreach (AudioSlider slider in sliders)
            {
                SaveVolume(slider);
            }
        }
        protected override void LoadVolume()
        {
            foreach (AudioSlider slider in sliders)
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
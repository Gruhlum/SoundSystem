using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HexTecGames.SoundSystem
{
    public class VolumeControl : SoundControl
    {
        [SerializeField] private string param = default;
        [SerializeField] private Slider slider = default;


        public void ToggleMute()
        {
            if (master.GetFloat(param, out float value))
            {
                if (value <= -80f)
                {
                    ChangeVolume(param, 0.5f);
                    slider.SetValueWithoutNotify(ConvertPercentToSliderValue(slider, 0.5f));
                }
                else
                {
                    ChangeVolume(param, 0);
                    slider.SetValueWithoutNotify(ConvertPercentToSliderValue(slider, 0f));

                }
            }
        }
        public void OnSliderValueChanged(float value)
        {
            ChangeVolume(param, ConvertSliderValueToPercent(slider));
        }
    }
}
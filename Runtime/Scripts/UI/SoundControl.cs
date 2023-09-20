using HexTecGames.Basics;
using HexTecGames.SoundSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace HexTecGames
{
	public abstract class SoundControl : MonoBehaviour
	{
        [SerializeField] protected AudioMixer master = default;

        protected void ChangeVolume(string param, float value)
        {
            if (master == null)
            {
                return;
            }
            master.SetFloat(param, ConvertPercentToLog(value));
        }
        protected void SaveVolume(AudioSlider slider)
        {
            if (master == null)
            {
                return;
            }
            if (master.GetFloat(slider.AudioParam, out float value))
            {
                SaveSystem.SaveSettings(slider.AudioParam, ConvertLogToPercent(value).ToString());
            }
        }
        protected void LoadVolume(AudioSlider slider)
        {
            if (master == null)
            {
                return;
            }
            string setting = SaveSystem.LoadSettings(slider.AudioParam);
            float volume = 0.9f;
            if (!string.IsNullOrEmpty(setting))
            {
                volume = (float)Convert.ToDouble(setting);
            }
            if (master != null)
            {
                master.SetFloat(slider.AudioParam, Mathf.Log(volume) * 20f);
            }
            slider.Slider.value = ConvertPercentToSliderValue(slider.Slider, volume);
        }

        protected float ConvertSliderValueToPercent(Slider slider)
        {
            return Mathf.Lerp(0f, 1f, slider.value / slider.maxValue);
        }
        protected float ConvertPercentToSliderValue(Slider slider, float percent)
        {
            return Mathf.Lerp(slider.minValue, slider.maxValue, percent);
        }
        protected float ConvertPercentToLog(float value)
        {
            if (value <= 0)
            {
                return -80;
            }
            else return Mathf.Log(value) * 20f;
        }
        protected float ConvertLogToPercent(float value)
        {
            return Mathf.Exp(value / 20f);
        }      
    }
}
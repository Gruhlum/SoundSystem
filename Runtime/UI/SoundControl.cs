using HexTecGames.Basics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace HexTecGames.SoundSystem
{
    public abstract class SoundControl : MonoBehaviour
    {
        [SerializeField] protected AudioMixer master = default;

        [Header("Settings")]
        [SerializeField] private bool saveOnDisable = true;

        public event Action<float> OnVolumeChanged;

        private bool valueChanged;

        protected virtual void Start()
        {
            LoadVolume();
        }
        protected virtual void OnEnable()
        {
            valueChanged = false;
        }
        protected virtual void OnDisable()
        {
            if (saveOnDisable && valueChanged)
            {
                SaveVolume();
            }
        }

        protected abstract void LoadVolume();
        public abstract void SaveVolume();

        protected void ChangeVolume(AudioSlider slider)
        {
            ChangeVolume(slider.AudioParam, ConvertSliderValueToPercent(slider.Slider));
        }
        protected void ChangeVolume(string param, float percent)
        {
            if (master == null)
            {
                return;
            }
            valueChanged = true;
            float volume = ConvertPercentToLog(percent);
            master.SetFloat(param, volume);
            OnVolumeChanged?.Invoke(volume);
        }
        protected void SaveVolume(AudioSlider slider)
        {
            SaveVolume(slider.AudioParam);
        }
        protected void SaveVolume(string param)
        {
            if (master == null)
            {
                return;
            }
            if (master.GetFloat(param, out float value))
            {
                SaveSystem.SaveSettings(param, ConvertLogToPercent(value).ToString());
            }
        }
        protected void LoadVolume(AudioSlider slider)
        {
            LoadVolume(slider.Slider, slider.AudioParam);
        }
        protected void LoadVolume(Slider slider, string param)
        {
            if (master == null)
            {
                return;
            }
            SaveSystem.LoadSettings(param, out string setting);
            float volume = 0.9f;
            if (!string.IsNullOrEmpty(setting))
            {
                volume = (float)Convert.ToDouble(setting);
            }
            if (master != null)
            {
                master.SetFloat(param, Mathf.Log(volume) * 20f);
            }
            slider.SetValueWithoutNotify(ConvertPercentToSliderValue(slider, volume));
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
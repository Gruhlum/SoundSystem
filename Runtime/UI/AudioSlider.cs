using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace HexTecGames.SoundSystem
{
	public class AudioSlider : MonoBehaviour
	{
        public string AudioParam
        {
            get
            {
                return audioParam;
            }
            set
            {
                audioParam = value;
            }
        }
        [SerializeField] private string audioParam = default;

        public Slider Slider
        {
            get
            {
                return slider;
            }
            set
            {
                slider = value;
            }
        }
        [SerializeField] private Slider slider = default;


        public UnityEvent<AudioSlider, float> OnValueChanged;

        private void Reset()
        {
            slider = GetComponent<Slider>();
        }
        private void Awake()
        {
            slider.onValueChanged.AddListener(OnSliderValueChanged);
        }
        private void OnSliderValueChanged(float value)
        {
            OnValueChanged?.Invoke(this, value);
        }
    }
}
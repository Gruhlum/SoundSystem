using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using HexTecGames.SoundSystem;
using System;

namespace HexTecGames._SoundExample
{
    public class TestButton : AdvancedBehaviour
    {
        [SerializeField] private SoundClipBase clip = default;
        [SerializeField] private TMP_Text textGUI = default;
        [Space]
        [SerializeField] private Slider slider = default;

        private SoundSource soundSource;

        public SoundSource SoundSource
        {
            get
            {
                return this.soundSource;
            }
            private set
            {
                this.soundSource = value;
            }
        }

        public event Action<TestButton> PlayStarting;

        private void OnValidate()
        {
            if (clip == null)
            {
                textGUI.text = "No Clip";
            }
            else textGUI.text = clip.name;
        }

        private void Awake()
        {
            slider.value = 0;
        }

        public void StartPlay()
        {
            if (clip == null)
            {
                return;
            }
            if (SoundSource != null)
            {
                if (SoundSource.Loop)
                {
                    SoundSource.Stop();
                    SoundSource = null;
                    return;
                }
                if (!SoundSource.Args.data.LimitInstances)
                {
                    SoundSource.Stop();
                }
            }
            this.enabled = true;
            this.SoundSource = clip.Play();
            this.SoundSource.OnDeactivated += SoundSource_OnDeactivated;
            slider.value = 0;
            slider.maxValue = SoundSource.AudioSource.clip.length;
            PlayStarting?.Invoke(this);
        }

        private void SoundSource_OnDeactivated(SoundSource soundSource)
        {
            soundSource.OnDeactivated -= SoundSource_OnDeactivated;
            this.SoundSource = null;
            slider.value = 0;
            if (this != null)
            {
                this.enabled = false;
            }
        }

        private void Update()
        {
            if (SoundSource == null)
            {
                return;
            }
            //Debug.Log(soundSource.AudioSource.time);

            slider.value = SoundSource.AudioSource.time;
        }
    }
}
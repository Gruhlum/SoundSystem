using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HecTecGames.SoundSystem
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundSource : MonoBehaviour
    {
        [SerializeField] [HideInInspector] private AudioSource audioSource = default;
        private SoundClip soundClip;

        public bool IsPlaying
        {
            get
            {
                return audioSource.isPlaying;
            }
        }

        private Coroutine fadeIn;
        private Coroutine fadeOut;

        public float Volume
        {
            get
            {
                return volume;
            }
            set
            {
                volume = value;
                audioSource.volume = volume;
            }
        }
        private float volume;


        public bool Loop
        {
            get
            {
                return loop;
            }
            set
            {
                loop = value;
                audioSource.loop = loop;
            }
        }
        private bool loop;


        private void Reset()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void Setup(SoundClip soundClip)
        {
            this.soundClip = soundClip;
            audioSource.clip = soundClip.audioClip;
            Volume = soundClip.volume;
            audioSource.pitch = soundClip.pitch;
            PlaySound();
            StartCoroutine(DisableAfter(soundClip.audioClip.length / soundClip.pitch));
        }

        public void PlaySound()
        {
            audioSource.Play();
        }
        public void StopSound()
        {
            audioSource.Stop();
        }
        public void StartFadeOut(float time)
        {
            if (IsPlaying == false)
            {
                return;
            }
            if (fadeOut != null)
            {
                return;
            }
            if (fadeIn != null)
            {
                StopCoroutine(fadeIn);
                fadeIn = null;
            }
            fadeOut = StartCoroutine(FadeOut(time));
        }
        public void StartFadeIn(float time)
        {
            if (fadeIn != null)
            {
                return;
            }
            if (Volume >= soundClip.volume)
            {
                return;
            }
            if (fadeOut != null)
            {
                StopCoroutine(fadeOut);
                fadeOut = null;
            }
            fadeIn = StartCoroutine(FadeIn(time));
        }
        IEnumerator DisableAfter(float time)
        {
            yield return new WaitForSeconds(time);
            gameObject.SetActive(false);
        }
        IEnumerator FadeIn(float time)
        {
            if (IsPlaying == false)
            {
                PlaySound();
            }            
            while (Volume < soundClip.volume)
            {
                Volume += Mathf.Min(soundClip.volume / time * Time.fixedDeltaTime, soundClip.volume - Volume);
                yield return new WaitForFixedUpdate();
            }
            fadeIn = null;
        }
        IEnumerator FadeOut(float time)
        {
            float startVol = audioSource.volume;
            while (Volume > 0)
            {
                Volume -= Mathf.Min(startVol / time * Time.fixedDeltaTime, Volume);
                yield return new WaitForFixedUpdate();
            }
            StopSound();
            fadeOut = null;
        }
    }
}
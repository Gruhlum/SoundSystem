using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HexTecGames.SoundSystem
{
    [RequireComponent(typeof(AudioSource))]
    [ExecuteAlways]
    public class SoundSource : MonoBehaviour
    {
        public AudioSource AudioSource
        {
            get
            {
                return this.audioSource;
            }

            set
            {
                this.audioSource = value;
            }
        }
        [SerializeField] private AudioSource audioSource = default;

        public SoundClip SoundClip
        {
            get
            {
                return this.soundClip;
            }
            private set
            {
                this.soundClip = value;
            }
        }
        private SoundClip soundClip;

        public SoundArgs Args
        {
            get
            {
                return args;
            }
            private set
            {
                args = value;
            }
        }
        private SoundArgs args;

        public bool IsPlaying
        {
            get
            {
                return AudioSource.isPlaying;
            }
        }

        private bool isFadingIn;
        private bool isFadingOut;

        public float Volume
        {
            get
            {
                return volume;
            }
            set
            {
                volume = value;
                AudioSource.volume = volume;
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
                AudioSource.loop = loop;
            }
        }
        private bool loop;

        public bool DeactiveAfterPlay
        {
            get
            {
                return deactivateAfterPlay;
            }
            set
            {
                deactivateAfterPlay = value;
            }
        }
        private bool deactivateAfterPlay = true;

        private bool isDelayed;
        private bool isStopping;

        private float currentTime;

        public bool IsActive
        {
            get
            {
                if (isDelayed)
                {
                    return true;
                }
                if (!IsPlaying || isFadingOut)
                {
                    return false;
                }
                return true;
            }
        }

        public event Action<SoundSource> OnFinishedPlaying;


        private void Awake()
        {
            if (audioSource.playOnAwake)
            {
                audioSource.playOnAwake = false;
            }
        }
        private void Reset()
        {
            AudioSource = GetComponent<AudioSource>();
        }
        private void OnDisable()
        {
            OnFinishedPlaying?.Invoke(this);
            ResetData();
        }

        private void ResetData()
        {          
            isStopping = false;
            isFadingIn = false;
            isFadingOut = false;
            isDelayed = false;
            currentTime = 0;
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }

        public void Play()
        {
            if (Args == null)
            {
                Debug.LogError("No Args provided!");
            }
            else Play(Args);
        }
        public void Play(SoundArgs args)
        {
            StartCoroutine(PlayCoroutine(args));
        }
        public void ApplyArgs(SoundArgs args)
        {
            Args = args;
            args.source = this;
            this.SoundClip = args.soundClip;
            AudioSource.clip = args.audioClip;
            AudioSource.outputAudioMixerGroup = SoundClip.audioMixerGroup;

            Loop = args.loop;
            Volume = SoundClip.Volume.Value * args.volumeMulti;
            AudioSource.pitch = SoundClip.Pitch.Value * args.pitchMulti;
            AudioSource.time = args.startPosition;
        }
        public IEnumerator PlayCoroutine(SoundArgs args)
        {
            ResetData();
            ApplyArgs(args);
            
            if (SoundClip == null || args.audioClip == null)
            {
                Debug.Log("No audioClip!");
                yield break;
            }
            
            if (args.delay > 0)
            {
                yield return new WaitForSeconds(args.delay);
            }

            if (AudioSource.pitch < 0)
            {
                AudioSource.timeSamples = AudioSource.clip.samples - 1;
            }
            AudioSource.Play();

            if (args.fadeIn > 0)
            {
                StartCoroutine(FadeIn(args.fadeIn));
            }
            if (Loop)
            {
                yield break;
            }

            float targetTime = args.audioClip.length / audioSource.pitch;

            yield return null;

            if (args.fadeOut > 0)
            {
                while (currentTime < targetTime - args.fadeOut)
                {
                    yield return null;
                    currentTime += Time.deltaTime;
                }
                yield return FadeOut(args.fadeOut);
            }
            while (currentTime < targetTime)
            {
                yield return null;
                currentTime += Time.deltaTime;
            }
            gameObject.SetActive(false);
        }
        private IEnumerator FadeIn(float length)
        {
            if (isFadingIn)
            {
                yield break;
            }
            isFadingIn = true;
            isFadingOut = false;
            float targetVol = Volume;
            Volume = 0;

            for (float i = 0; i < length;)
            {
                if (isFadingIn == false)
                {
                    yield break;
                }
                Volume = Mathf.Lerp(0, targetVol, i / length);
                yield return null;
                i += Time.deltaTime;
            }
            Volume = targetVol;
            isFadingIn = false;
        }
        private IEnumerator FadeOut(float length)
        {
            if (isFadingOut)
            {
                yield break;
            }
            isFadingIn = false;
            isFadingOut = true;
            float startVolume = Volume;

            for (float i = 0; i < length;)
            {
                if (isFadingOut == false)
                {
                    yield break;
                }
                Volume = Mathf.Lerp(startVolume, 0, i / length);
                yield return null;
                i += Time.deltaTime;
            }
            AudioSource.Stop();
            isFadingOut = false;
        }
        public void Stop(float delay = 0, float fadeOut = 0)
        {
            if (isStopping)
            {
                return;
            }
            if (gameObject.activeInHierarchy)
            {
                StartCoroutine(StopCoroutine(delay, fadeOut));
            }            
        }
        private IEnumerator StopCoroutine(float delay, float fadeOut)
        {
            isStopping = true;
            if (this == null || gameObject == null)
            {
                yield break;
            }
            if (delay > 0)
            {
                yield return new WaitForSeconds(delay);
            }
            if (fadeOut > 0)
            {
                yield return FadeOut(fadeOut);
            }
            if (audioSource != null)
            {
                AudioSource.Stop();
            }
            gameObject.SetActive(false);
            isStopping = false;
        }
    }
}
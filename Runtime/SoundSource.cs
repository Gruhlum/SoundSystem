using System;
using System.Collections;
using HexTecGames.Basics;
using UnityEngine;

namespace HexTecGames.SoundSystem
{
    [RequireComponent(typeof(AudioSource))]
    [ExecuteAlways]
    public class SoundSource : MonoBehaviour, ISpawnable<SoundSource>
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

        //public SoundClip SoundClip
        //{
        //    get
        //    {
        //        return this.soundClip;
        //    }
        //    private set
        //    {
        //        this.soundClip = value;
        //    }
        //}
        //private SoundClip soundClip;

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

        //public event Action<SoundSource> OnFinishedPlaying;
        public event Action<SoundSource> OnDeactivated;

        private void Awake()
        {
            if (AudioSource == null)
            {
                AudioSource = GetComponent<AudioSource>();
            }
            if (AudioSource.playOnAwake)
            {
                AudioSource.playOnAwake = false;
            }
        }
        private void Reset()
        {
            AudioSource = GetComponent<AudioSource>();
        }
        private void OnDisable()
        {
            OnDeactivated?.Invoke(this);
            //OnFinishedPlaying?.Invoke(this);
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
            if (args.audioClip == null)
            {
                gameObject.name = "No AudioClip";
            }
            else gameObject.name = args.audioClip.name;
            StartCoroutine(PlayCoroutine(args));
        }
        public void ApplyArgs(SoundArgs args)
        {
            Args = args;
            args.source = this;
            //this.SoundClip = args.soundClip;
            AudioSource.clip = args.audioClip;
            AudioSource.outputAudioMixerGroup = args.audioMixerGroup;

            Loop = args.loop;
            Volume = args.data.Volume;
            AudioSource.pitch = args.data.Pitch;

            AudioSource.time = args.data.StartPosition;
        }
        public IEnumerator PlayCoroutine(SoundArgs args)
        {
            ResetData();
            ApplyArgs(args);

            SoundArgsData data = args.data;

            if (args.audioClip == null)
            {
                Debug.Log("No audioClip!");
                yield break;
            }

            if (data.Delay > 0)
            {
                yield return new WaitForSeconds(data.Delay);
            }

            if (AudioSource.pitch < 0)
            {
                AudioSource.timeSamples = AudioSource.clip.samples - 1;
            }
            AudioSource.Play();

            if (data.FadeIn > 0)
            {
                StartCoroutine(FadeIn(data.FadeIn));
            }
            if (Loop)
            {
                yield break;
            }

            float targetTime = args.audioClip.length / audioSource.pitch;

            yield return null;

            if (data.FadeOut > 0)
            {
                while (currentTime < targetTime - data.FadeOut)
                {
                    yield return null;
                    currentTime += Time.deltaTime;
                }
                yield return FadeOut(data.FadeOut);
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
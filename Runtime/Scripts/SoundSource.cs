using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

namespace HexTecGames.SoundSystem
{
    [RequireComponent(typeof(AudioSource))]
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
        [SerializeField][HideInInspector] private AudioSource audioSource = default;
        private SoundClipBase soundClip;

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

        private bool isDelayed;

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
      

        private void Reset()
        {
            AudioSource = GetComponent<AudioSource>();
        }

        public void PlaySound(SoundArgs args)
        {
            args.source = this;
            this.soundClip = args.soundClip;
            AudioSource.clip = soundClip.GetAudioClip();
            AudioSource.outputAudioMixerGroup = soundClip.audioMixerGroup;

            Loop = args.loop;
            Volume = soundClip.volume * args.volMulti;
            AudioSource.pitch = soundClip.pitch * args.pitchMulti;


            if (args.delay > 0)
            {
                StartCoroutine(PlayDelayed(args.delay, args.fadeIn));
            }
            else
            {
                StartPlaying(args.fadeIn);
            }
        }
        private void StartPlaying(float fadeIn)
        {
            AudioClip audioClip = soundClip.GetAudioClip();
            if (soundClip == null || audioClip == null)
            {
                return;
            }
            isFadingOut = false;
            if (fadeIn > 0)
            {
                StartCoroutine(FadeIn(fadeIn));
            }
            if (Loop == false)
            {
                StartCoroutine(DisableAfter(audioClip.length / Mathf.Abs(soundClip.pitch)));
            }
            if (AudioSource.pitch < 0)
            {
                AudioSource.timeSamples = AudioSource.clip.samples - 1;
                AudioSource.Play();
            }
            else AudioSource.Play();
        }
        private IEnumerator PlayDelayed(float delay, float fadeIn)
        {
            isDelayed = true;
            yield return new WaitForSeconds(delay);
            isDelayed = false;
            StartPlaying(fadeIn);
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

            for (float i = 0; i < length; i += Time.fixedDeltaTime)
            {
                if (isFadingIn == false)
                {
                    yield break;
                }
                Volume = Mathf.Lerp(0, targetVol, i / length);
                yield return new WaitForFixedUpdate();
            }
            isFadingIn = false;
        }

        public void StopSound(float delay, float fadeOut)
        {
            if (delay > 0)
            {
                StartCoroutine(StopDelayed(delay, fadeOut));
            }
            else
            {
                if (fadeOut > 0)
                {
                    StartCoroutine(FadeOut(fadeOut));
                }
                AudioSource.Stop();
            }
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

            for (float i = 0; i < length; i += Time.fixedDeltaTime)
            {
                if (isFadingOut == false)
                {
                    yield break;
                }
                Volume = Mathf.Lerp(startVolume, 0, i / length);
                yield return new WaitForFixedUpdate();
            }
            AudioSource.Stop();
            isFadingOut = false;
        }
        private IEnumerator StopDelayed(float delay, float fadeOut)
        {
            yield return new WaitForSeconds(delay);
            if (fadeOut > 0)
            {
                StopCoroutine(FadeOut(fadeOut));
            }
            else
            {
                AudioSource.Stop();
            }          
        }

        private IEnumerator DisableAfter(float time)
        {
            yield return new WaitForSeconds(time + 0.2f);
            gameObject.SetActive(false);
        }
    }
}
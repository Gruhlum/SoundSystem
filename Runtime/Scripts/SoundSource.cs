using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HecTecGames.SoundSystem
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundSource : MonoBehaviour
    {
        [SerializeField][HideInInspector] private AudioSource audioSource = default;
        private SoundClip soundClip;

        public bool IsPlaying
        {
            get
            {
                return audioSource.isPlaying;
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
            audioSource = GetComponent<AudioSource>();
        }

        public void Setup(SoundClip soundClip)
        {
            this.soundClip = soundClip;
            audioSource.clip = soundClip.audioClip;
        }

        public void PlaySound(float delay, float fadeIn, float volMulti, float pitchMulti, bool loop)
        {
            Loop = loop;
            Volume = soundClip.volume * volMulti;
            audioSource.pitch = soundClip.pitch * pitchMulti;

            if (delay > 0)
            {
                StartCoroutine(PlayDelayed(delay, fadeIn));
            }
            else
            {
                StartPlaying(fadeIn);
            }
        }
        private void StartPlaying(float fadeIn)
        {
            isFadingOut = false;
            if (fadeIn > 0)
            {
                StartCoroutine(FadeIn(fadeIn));
            }
            if (Loop == false)
            {
                StartCoroutine(DisableAfter(soundClip.audioClip.length / Mathf.Abs(soundClip.pitch)));
            }
            if (audioSource.pitch < 0)
            {
                audioSource.timeSamples = audioSource.clip.samples - 1;
                audioSource.Play();
            }
            else audioSource.Play();
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
                audioSource.Stop();
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
            audioSource.Stop();
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
                audioSource.Stop();
            }          
        }

        private IEnumerator DisableAfter(float time)
        {
            yield return new WaitForSeconds(time);
            //gameObject.SetActive(false);
        }
    }
}
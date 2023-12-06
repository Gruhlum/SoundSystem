using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        private float GetScaledLength()
        {
            return GetScaledLength(audioSource.clip.length);
        }
        private float GetScaledLength(float length)
        {
            return  (length - Args.startPosition) / Mathf.Abs(SoundClip.Pitch);
        }
        public void Play(SoundArgs args)
        {
            Args = args;
            args.source = this;
            this.SoundClip = args.soundClip;
            AudioSource.clip = args.audioClip;
            AudioSource.outputAudioMixerGroup = SoundClip.audioMixerGroup;

            Loop = args.loop;
            Volume = SoundClip.Volume * args.volumeMulti;
            AudioSource.pitch = SoundClip.Pitch * args.pitchMulti;
            AudioSource.time = args.startPosition;
            if (args.delay > 0)
            {
                StartCoroutine(PlayDelayed(args));
            }
            else
            {
                StartPlaying(args);
            }
        }
        private void StartPlaying(SoundArgs args)
        {
            AudioClip audioClip = args.audioClip;
            if (SoundClip == null || audioClip == null)
            {
                return;
            }
            isFadingOut = false;        
            if (args.fadeIn > 0)
            {
                StartCoroutine(FadeIn(args.fadeIn));
            }
            if (Loop == false)
            {
                StartCoroutine(DisableAfter(GetScaledLength()));
                if (args.fadeOut > 0)
                {
                    float delay = GetScaledLength(audioClip.length - args.fadeOut);
                    Stop(delay, args.fadeOut);
                }              
            }
            if (AudioSource.pitch < 0)
            {
                AudioSource.timeSamples = AudioSource.clip.samples - 1;
                AudioSource.Play();
            }
            else AudioSource.Play();
        }
        private IEnumerator PlayDelayed(SoundArgs args)
        {
            isDelayed = true;
            yield return new WaitForSeconds(args.delay);
            isDelayed = false;
            StartPlaying(args);
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

        public void Stop(float delay = 0, float fadeOut = 0)
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
                gameObject.SetActive(false);
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
                StartCoroutine(FadeOut(fadeOut));
            }
            else
            {
                AudioSource.Stop();
                gameObject.SetActive(false);
            }          
        }

        private IEnumerator DisableAfter(float time)
        {
            if (!DeactiveAfterPlay)
            {
                yield break;
            }
            yield return new WaitForSeconds(time + 0.2f);
            if (!DeactiveAfterPlay)
            {
                yield break;
            }
            gameObject.SetActive(false);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.SoundSystem
{
    public class ClipPlayer : MonoBehaviour
    {
        [SerializeField] protected SoundClip soundClip = default;
        //private bool init;

        public bool PlayOnStart
        {
            get
            {
                return playOnStart;
            }
            set
            {
                playOnStart = value;
            }
        }
        [SerializeField] private bool playOnStart;

        public bool StopOnDestroy
        {
            get
            {
                return stopOnDestroy;
            }
            set
            {
                stopOnDestroy = value;
            }
        }
        [SerializeField] private bool stopOnDestroy;


        protected SoundSource source;

        private void Start()
        {
            if (PlayOnStart)
            {
                Play();
            }
        }
        void OnDestroy()
        {
            if (StopOnDestroy)
            {
                Stop();
            }
        }
        public virtual void Play()
        {
            SoundArgs args = new SoundArgs(soundClip);
            soundClip.Play(args);
            source = args.source;
        }
        public virtual void Stop()
        {
            if (source != null)
            {
                source.Stop();
                source = null;
            }
        }
        //private void OnEnable()
        //{
        //    SoundArgs args = new SoundArgs(clip, FadeIn, Delay, 1f, 1f, Loop);
        //    clip.Play(FadeIn, Delay, loop: true);
        //    if (!args.failed)
        //    {
        //        init = true;
        //    }
        //}
    }
}
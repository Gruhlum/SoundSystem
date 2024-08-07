using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.SoundSystem
{
    public class ClipPlayer : MonoBehaviour
    {
        [SerializeField] protected SoundClip soundClip = default;
        //private bool init;

        public PlayMode PlayMode
        {
            get
            {
                return playMode;
            }
            private set
            {
                playMode = value;
            }
        }
        [SerializeField] private PlayMode playMode;

        public StopMode StopMode
        {
            get
            {
                return stopMode;
            }
            private set
            {
                stopMode = value;
            }
        }
        [SerializeField] private StopMode stopMode;


        protected SoundSource source;

        private void Start()
        {
            if (PlayMode == PlayMode.Start)
            {
                Play();
            }
        }
        private void OnEnable()
        {
            if (PlayMode == PlayMode.Enable)
            {
                Play();
            }
        }
        private void OnDisable()
        {
            if (StopMode == StopMode.Disable)
            {
                Stop();
            }
        }
        private void OnDestroy()
        {
            if (StopMode == StopMode.Destroy)
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
using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics;
using UnityEngine;

namespace HexTecGames.SoundSystem
{
    public class ClipPlayer : MonoBehaviour
    {
        [SerializeField] protected SoundClip soundClip = default;

        public UnityEventType PlayMode
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
        [SerializeField] private UnityEventType playMode;

        public UnityEventType StopMode
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
        [SerializeField] private UnityEventType stopMode;


        protected SoundSource source;

        private void Start()
        {
            if (PlayMode == UnityEventType.Start)
            {
                Play();
            }
            if (StopMode == UnityEventType.Start)
            {
                Stop();
            }
        }
        private void OnEnable()
        {
            if (PlayMode == UnityEventType.Enable)
            {
                Play();
            }
            if (StopMode == UnityEventType.Enable)
            {
                Stop();
            }
        }
        private void OnDisable()
        {
            if (PlayMode == UnityEventType.Disable)
            {
                Play();
            }
            if (StopMode == UnityEventType.Disable)
            {
                Stop();
            }
        }
        private void OnDestroy()
        {
            if (PlayMode == UnityEventType.Destroy)
            {
                Play();
            }
            if (StopMode == UnityEventType.Destroy)
            {
                Stop();
            }
        }
        public virtual void Play()
        {
            SoundArgs args = new SoundArgs(soundClip);
            soundClip.Play(args);
            source = args.source;
            source.OnDeactivated += Source_OnDeactivated;
        }

        public virtual void Stop()
        {
            if (source != null)
            {
                source.Stop();
                ClearSource();
            }
        }
        private void ClearSource()
        {
            if (source != null)
            {
                source.OnDeactivated -= Source_OnDeactivated;
                source = null;
            }
        }
        private void Source_OnDeactivated(ISpawnable obj)
        {
            ClearSource();
        }
    }
}
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
            CheckForTrigger(UnityEventType.Start);
        }

        private void OnEnable()
        {
            CheckForTrigger(UnityEventType.Enable);
        }
        private void OnDisable()
        {
            CheckForTrigger(UnityEventType.Disable);
        }
        private void OnDestroy()
        {
            CheckForTrigger(UnityEventType.Destroy);
        }

        private void CheckForTrigger(UnityEventType triggerType)
        {
            if (PlayMode.HasFlag(triggerType))
            {
                Play();
            }
            else if (StopMode.HasFlag(triggerType))
            {
                Stop();
            }
        }

        public virtual void Play()
        {
            source = soundClip.Play();
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
        private void Source_OnDeactivated(SoundSource obj)
        {
            ClearSource();
        }
    }
}
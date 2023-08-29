using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.SoundSystem
{
	public class ClipPlayer : MonoBehaviour
	{
        [SerializeField] private SoundClipBase clip = default;

        public float FadeIn;
        public float Delay;
        public bool Loop;
        private bool init;

        private void Start()
        {
            if (init == false)
            {
                clip.Play(FadeIn, Delay, loop: true);
            }
        }

        private void OnEnable()
        {
            SoundArgs args = new SoundArgs(clip, FadeIn, Delay, 1f, 1f, Loop);
            clip.Play(FadeIn, Delay, loop: true);
            if (!args.failed)
            {
                init = true;
            }
        }
    }
}
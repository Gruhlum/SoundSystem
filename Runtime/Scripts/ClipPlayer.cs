using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HecTecGames.SoundSystem
{
	public class ClipPlayer : MonoBehaviour
	{
        [SerializeField] private SoundClip Clip = default;

        public float FadeIn;
        public float Delay;

        private bool init;

        private void Start()
        {
            if (init == false)
            {
                Clip.Play(FadeIn, Delay);
            }
        }

        private void OnEnable()
        {
            SoundArgs args = Clip.Play(FadeIn, Delay);
            if (!args.failed)
            {
                init = true;
            }
        }
    }
}
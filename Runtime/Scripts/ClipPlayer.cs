using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.SoundSystem
{
	public class ClipPlayer : MonoBehaviour
	{
        [SerializeField] protected SoundClipBase soundClip = default;
        //private bool init;

        private void Start()
        {
            soundClip.Play();
            //if (!args.failed)
            //{
            //    init = true;
            //}
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
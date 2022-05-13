using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HecTecGames.SoundSystem.Example1
{
    public class Engine : MonoBehaviour
    {
        public PersistentSound persistentSound;

        [SerializeField] private SpriteRenderer sr = default;

        private void Reset()
        {
            sr = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            //if (Input.GetKey(KeyCode.W))
            //{
            //    persistentSound.FadeIn(1f);                
            //}
            //else persistentSound.FadeOut(1f);

            //float colorValue = 1f - persistentSound.SoundSource.Volume / persistentSound.soundClip.volume;
            //sr.color = new Color(1f, colorValue, colorValue);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HecTecGames.SoundSystem.Example1
{
	public class Engine : MonoBehaviour
	{
		public PersistentSound persistentSound;

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                persistentSound.FadeIn(1f);
            }
            else persistentSound.FadeOut(1f);
        }
    }
}
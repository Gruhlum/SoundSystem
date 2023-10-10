using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HexTecGames.SoundSystem
{
	public class InputSoundController : MonoBehaviour
	{
        [SerializeField] private EventSystem es = default;

        [SerializeField] private SoundClipBase inputSound = default;

        private GameObject lastSelected;

        private void Update()
        {
            if (inputSound == null)
            {
                return;
            }
            if (lastSelected != null && lastSelected.activeInHierarchy == false)
            {
                lastSelected = null;
            }

            GameObject currentSelected = es.currentSelectedGameObject;

            if (currentSelected == null)
            {
                return;
            }
            if (currentSelected != lastSelected)
            {
                if (lastSelected != null)
                {
                    inputSound.Play();
                }
                lastSelected = es.currentSelectedGameObject;

            }
        }
    }
}
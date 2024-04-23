using HexTecGames.SoundSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HexTecGames
{
	[RequireComponent(typeof(Button))]
	public class ButtonSoundPlayer : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler
	{
		[SerializeField] private SoundClip clickClip = default;
		[SerializeField] private SoundClip hoverEnterClip = default;
		[SerializeField] private SoundClip hoverExitClip = default;

		[SerializeField] private Button btn = default;

        public bool PlayWhenDisabled
        {
            get
            {
                return playWhenDisabled;
            }
            set
            {
                playWhenDisabled = value;
            }
        }
        [SerializeField] private bool playWhenDisabled = default;

        private void Reset()
        {
            btn = GetComponent<Button>();
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            if (!playWhenDisabled && !btn.interactable)
            {
                return;
            }
            if (clickClip != null)
            {
                clickClip.Play();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!playWhenDisabled && !btn.interactable)
            {
                return;
            }
            if (hoverEnterClip != null)
            {
                hoverEnterClip.Play();
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!playWhenDisabled && !btn.interactable)
            {
                return;
            }
            if (hoverExitClip != null)
            {
                hoverExitClip.Play();
            }
        }

        
    }
}
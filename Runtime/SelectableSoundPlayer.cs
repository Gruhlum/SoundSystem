using System.Collections;
using System.Collections.Generic;
using HexTecGames;
using UnityEngine;
using UnityEngine.EventSystems;


namespace HexTecGames.SoundSystem
{
    public class SelectableSoundPlayer : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
    {
        [SerializeField] private SoundClipBase hoverSound = default;
        [SerializeField] private SoundClipBase clickSound = default;

        public void OnPointerClick(PointerEventData eventData)
        {
            clickSound?.Play();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            hoverSound?.Play();
        }
    }
}
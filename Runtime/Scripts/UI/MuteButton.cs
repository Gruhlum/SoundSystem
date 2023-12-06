using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.SoundSystem
{
	public class MuteButton : MonoBehaviour
	{
		[SerializeField] private GameObject crossGO = default;
		[SerializeField] private VolumeControl volumeControl = default;


        private void Awake()
        {
            volumeControl.OnVolumeChanged += VolumeControl_OnVolumeChanged;
        }

        private void VolumeControl_OnVolumeChanged(float volume)
        {
            crossGO.gameObject.SetActive(volume <= -80);
        }

        public void OnClicked()
		{
			volumeControl.ToggleMute();		
        }
	}
}
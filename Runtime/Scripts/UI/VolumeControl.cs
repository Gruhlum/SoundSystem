using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HexTecGames.SoundSystem
{
	public class VolumeControl : SoundControl
    {
		[SerializeField] private string param = default;
		[SerializeField] private Slider slider = default;
		public void OnMuteButtonClicked()
		{
			if (master.GetFloat(param, out float value))
			{
				if (value <= -80f)
				{
					master.SetFloat(param, ConvertPercentToLog(0.5f));
                    slider.SetValueWithoutNotify(ConvertPercentToSliderValue(slider, 0.5f));
                }
				else
				{
                    master.SetFloat(param, -80f);
                    slider.SetValueWithoutNotify(ConvertPercentToSliderValue(slider, 0f));
                } 
				

            }
		}
        public void OnSliderValueChanged(float value)
		{
            ChangeVolume(param, ConvertSliderValueToPercent(slider));
        }
	}
}
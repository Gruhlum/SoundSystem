using HexTecGames.Basics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.SoundSystem
{
	[System.Serializable]
	public class FloatValue
	{
        public ValueMode Mode
        {
            get
            {
                return mode;
            }
            private set
            {
                mode = value;
            }
        }
        [SerializeField] private ValueMode mode;

        public float Value
        {
            get
            {
                if (Mode == ValueMode.Flat)
                {
                    return Flat;
                }
                else return Random.Range(Min, Max);
            }
        }

        private float Flat
        {
            get
            {
                return flat;
            }
            set
            {
                flat = value;
            }
        }
        [SerializeField, DrawIf(nameof(mode), ValueMode.Flat)] private float flat = 1f;
        private float Min
        {
            get
            {
                return min;
            }
            set
            {
                min = value;
            }
        }
        [SerializeField, DrawIf(nameof(mode), ValueMode.Random)] private float min = 1f;

        private float Max
        {
            get
            {
                return max;
            }
            set
            {
                max = value;
            }
        }
        [SerializeField, DrawIf(nameof(mode), ValueMode.Random)] private float max = 1f;

        private float sliderMin;
        private float sliderMax;

        public FloatValue(float sliderMin, float sliderMax)
        {
            if (sliderMin > sliderMax)
            {
                sliderMin = sliderMax;
            }
            this.sliderMin = sliderMin;
            this.sliderMax = sliderMax;
        }
    }
}
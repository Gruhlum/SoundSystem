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
                else return Random.Range(Range.x, Range.y);
            }
            set
            {
                Flat = value;
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
        [SerializeField] private float flat = 1f;
        //private float Min
        //{
        //    get
        //    {
        //        return min;
        //    }
        //    set
        //    {
        //        min = value;
        //    }
        //}
        //[SerializeField] private float min = 0f;

        //private float Max
        //{
        //    get
        //    {
        //        return max;
        //    }
        //    set
        //    {
        //        max = value;
        //    }
        //}
        //[SerializeField] private float max = 1f;

        public Vector2 Range
        {
            get
            {
                return range;
            }
            private set
            {
                range = value;
            }
        }
        [SerializeField] private Vector2 range;


        [SerializeField, HideInInspector] private float sliderMin = 0f;
        [SerializeField, HideInInspector] private float sliderMax = 1f;

        public FloatValue()
        {
        }
        public FloatValue(float sliderMin, float sliderMax) : this()
        {
            SetSliderRange(sliderMin, sliderMax);
            range = new Vector2(0.8f, sliderMax * 0.8f);
        }

        public void SetSliderRange(float sliderMin, float sliderMax)
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
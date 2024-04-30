using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace HexTecGames
{
    public class CustomMinMaxSlider : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<CustomMinMaxSlider> { }

        private FloatField minField;
        private FloatField maxField;
        private MinMaxSlider minMaxSlider;

        public float LowLimit
        {
            get
            {
                return minMaxSlider.lowLimit;
            }
            set
            {
                minMaxSlider.lowLimit = value;
            }
        }
        public float HighLimit
        {
            get
            {
                return minMaxSlider.highLimit;
            }
            set
            {
                minMaxSlider.highLimit = value;
            }
        }


        public CustomMinMaxSlider()
        {
            VisualTreeAsset tree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Packages/com.hextecgames.soundsystem/Editor/UI Toolkit/CustomMinMaxSlider.uxml");
            if (tree == null)
            {
                Debug.Log("Wrong Path!");
                return;
            }
            tree.CloneTree(this);
            minMaxSlider = this.Q<MinMaxSlider>("RandomSlider");
            minMaxSlider.RegisterValueChangedCallback(SliderValueChanged);

            minField = this.Q<FloatField>("MinValue");
            minField.RegisterCallback<FocusOutEvent>(MinFieldFocusOut);

            maxField = this.Q<FloatField>("MaxValue");
            maxField.RegisterCallback<FocusOutEvent>(MaxFieldFocusOut);
        }
        private void MinFieldFocusOut(FocusOutEvent args)
        {
            if (minField.value < minMaxSlider.lowLimit)
            {
                minField.value = minMaxSlider.lowLimit;
            }
            minMaxSlider.minValue = minField.value;
        }
        private void MaxFieldFocusOut(FocusOutEvent args)
        {
            if (maxField.value > minMaxSlider.highLimit)
            {
                maxField.value = minMaxSlider.highLimit;
            }
            minMaxSlider.maxValue = maxField.value;
        }
        private void SliderValueChanged(ChangeEvent<Vector2> args)
        {
            minField.value = RoundToDigits(args.newValue.x, 2);
            maxField.value = RoundToDigits(args.newValue.y, 2);
        }
        private float RoundToDigits(float input, int digits)
        {
            // input = 0.146223, digits = 3
            float power = Mathf.Pow(10, digits); // = 1000
            input *= 10 * power; // 0.146223 * 1000 =  146.223
            input = Mathf.Round(input); // = 146
            input /= 10 * power; // 146 / 1000 = 0.146

            return input;
        }
    }
}
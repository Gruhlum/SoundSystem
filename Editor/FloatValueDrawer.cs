using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace HexTecGames.SoundSystem
{
    [CustomPropertyDrawer(typeof(FloatValue))]
    public class FloatValueDrawer : PropertyDrawer
    {
        private EnumField enumField;

        private VisualElement randomSliderParent;
        private VisualElement flatSliderParent;

        private Slider flatSlider;
        private CustomMinMaxSlider minMaxSlider;

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement root = new VisualElement();
            VisualTreeAsset document = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Packages/com.hextecgames.soundsystem/Editor/UI Toolkit/FloatValueDrawer.uxml");
            if (document == null)
            {
                Debug.Log("Wrong Path!");
                return root;
            }
            document.CloneTree(root);

            Label label = root.Q<Label>("Name");
            label.text = property.displayName;

            enumField = root.Q<EnumField>(name: "EnumField");
            enumField.RegisterValueChangedCallback(EnumValueChanged);
            flatSlider = root.Q<Slider>("FlatSlider");
            flatSliderParent = root.Q<VisualElement>("FlatSliderParent");

            flatSlider.lowValue = property.FindPropertyRelative("sliderMin").floatValue;
            flatSlider.highValue = property.FindPropertyRelative("sliderMax").floatValue;

            randomSliderParent = root.Q<VisualElement>("RandomSliderParent");
            minMaxSlider = root.Q<CustomMinMaxSlider>("CustomMinMaxSlider");

            minMaxSlider.LowLimit = property.FindPropertyRelative("sliderMin").floatValue;
            minMaxSlider.HighLimit = property.FindPropertyRelative("sliderMax").floatValue;

            DisplayCorrectSlider((ValueMode)property.FindPropertyRelative("mode").enumValueIndex);
            return root;
        }

        private void EnumValueChanged(ChangeEvent<System.Enum> args)
        {
            DisplayCorrectSlider((ValueMode)args.newValue);
        }

        private void DisplayCorrectSlider(ValueMode mode)
        {
            //Debug.Log(mode);
            if (mode != ValueMode.Flat)
            {
                flatSliderParent.style.display = DisplayStyle.None;
                randomSliderParent.style.display = DisplayStyle.Flex;
            }
            else
            {
                flatSliderParent.style.display = DisplayStyle.Flex;
                randomSliderParent.style.display = DisplayStyle.None;
            }
        }
    }
}
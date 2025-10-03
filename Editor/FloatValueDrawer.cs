using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace HexTecGames.SoundSystem
{
    [CustomPropertyDrawer(typeof(FloatValue))]
    public class FloatValueDrawer : PropertyDrawer
    {
        //private EnumField enumField;

        //private VisualElement randomSliderParent;
        //private VisualElement flatSliderParent;

        //private Slider flatSlider;
        //private CustomMinMaxSlider minMaxSlider;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            // Draw label manually for pixel-perfect alignment
            Rect labelRect = new Rect(position.x, position.y, EditorGUIUtility.labelWidth, position.height);
            EditorGUI.LabelField(labelRect, label);

            // Remaining content area
            float x = position.x + EditorGUIUtility.labelWidth;
            float contentWidth = position.width - EditorGUIUtility.labelWidth;
            Rect contentRect = new Rect(x + 3, position.y, contentWidth - 4, position.height);

            // Constants
            float dropdownWidth = 70f;
            float spacing = 5f;
            float fieldWidth = 50f;

            // Get properties
            var modeProp = property.FindPropertyRelative("mode");
            var valueProp = property.FindPropertyRelative("flat");
            var range = property.FindPropertyRelative("range");
            var sliderMin = property.FindPropertyRelative("sliderMin");
            var sliderMax = property.FindPropertyRelative("sliderMax");

            // Dropdown
            Rect dropdownRect = new Rect(contentRect.x, contentRect.y, dropdownWidth, contentRect.height);
            EditorGUI.PropertyField(dropdownRect, modeProp, GUIContent.none);

            float contentX = contentRect.x + dropdownWidth + spacing;
            float contentRemaining = contentRect.width - dropdownWidth - spacing;

            if ((ValueMode)modeProp.enumValueIndex == ValueMode.Flat)
            {
                // Flat: Slider only (includes float field)
                Rect sliderRect = new Rect(contentX, contentRect.y, contentRemaining, contentRect.height);
                EditorGUI.Slider(sliderRect, valueProp, sliderMin.floatValue, sliderMax.floatValue, GUIContent.none);
            }
            else
            {
                // Random: Min field → MinMaxSlider → Max field
                float sliderWidth = contentRemaining - fieldWidth * 2 - spacing * 2;

                Rect minRect = new Rect(contentX, contentRect.y, fieldWidth, contentRect.height);
                Rect sliderRect = new Rect(contentX + fieldWidth + spacing, contentRect.y, sliderWidth, contentRect.height);
                Rect maxRect = new Rect(contentX + fieldWidth + spacing + sliderWidth + spacing, contentRect.y, fieldWidth, contentRect.height);

                float minVal = Mathf.Round(range.vector2Value.x * 1000f) / 1000f;
                float maxVal = Mathf.Round(range.vector2Value.y * 1000f) / 1000f;

                minVal = EditorGUI.FloatField(minRect, minVal);
                EditorGUI.MinMaxSlider(sliderRect, ref minVal, ref maxVal, sliderMin.floatValue, sliderMax.floatValue);
                maxVal = EditorGUI.FloatField(maxRect, maxVal);

                range.vector2Value = new Vector2(minVal, maxVal);
            }
            EditorGUI.EndProperty();
        }
        //public override VisualElement CreatePropertyGUI(SerializedProperty property)
        //{
        //    VisualElement root = new VisualElement();
        //    VisualTreeAsset document = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Packages/com.hextecgames.soundsystem/Editor/UI Toolkit/FloatValueDrawer.uxml");
        //    if (document == null)
        //    {
        //        Debug.Log("Wrong Path!");
        //        return root;
        //    }
        //    document.CloneTree(root);

        //    Label label = root.Q<Label>("Name");
        //    label.text = property.displayName;

        //    enumField = root.Q<EnumField>(name: "EnumField");
        //    enumField.RegisterValueChangedCallback(EnumValueChanged);
        //    flatSlider = root.Q<Slider>("FlatSlider");
        //    flatSliderParent = root.Q<VisualElement>("FlatSliderParent");

        //    flatSlider.lowValue = property.FindPropertyRelative("sliderMin").floatValue;
        //    flatSlider.highValue = property.FindPropertyRelative("sliderMax").floatValue;

        //    randomSliderParent = root.Q<VisualElement>("RandomSliderParent");
        //    minMaxSlider = root.Q<CustomMinMaxSlider>("CustomMinMaxSlider");

        //    minMaxSlider.LowLimit = property.FindPropertyRelative("sliderMin").floatValue;
        //    minMaxSlider.HighLimit = property.FindPropertyRelative("sliderMax").floatValue;

        //    DisplayCorrectSlider((ValueMode)property.FindPropertyRelative("mode").enumValueIndex);
        //    return root;
        //}

        //private void EnumValueChanged(ChangeEvent<System.Enum> args)
        //{
        //    DisplayCorrectSlider((ValueMode)args.newValue);
        //}

        //private void DisplayCorrectSlider(ValueMode mode)
        //{
        //    //Debug.Log(mode);
        //    if (mode != ValueMode.Flat)
        //    {
        //        flatSliderParent.style.display = DisplayStyle.None;
        //        randomSliderParent.style.display = DisplayStyle.Flex;
        //    }
        //    else
        //    {
        //        flatSliderParent.style.display = DisplayStyle.Flex;
        //        randomSliderParent.style.display = DisplayStyle.None;
        //    }
        //}
    }
}
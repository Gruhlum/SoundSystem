using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace HexTecGames.SoundSystem
{
    //[CustomPropertyDrawer(typeof(FloatValue))]
    public class FloatValueDrawer : PropertyDrawer
    {
        //public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        //{
        //    EditorGUILayout.BeginHorizontal(GUILayout.Height(20));


        //    //ValueMode mode = (ValueMode)(property.FindPropertyRelative("mode").enumValueIndex);
        //    EditorGUILayout.PropertyField(property.FindPropertyRelative("mode"));
        //    return;
        //    //EditorGUILayout.FloatField(property.FindPropertyRelative("mode"));
        //    EditorGUILayout.FloatField(property.FindPropertyRelative("flat").floatValue);


        //    EditorGUILayout.LabelField("Upgradable", GUILayout.Width(68));
        //    bool isUpgradeable = property.FindPropertyRelative("isUpgradeable").boolValue;
        //    isUpgradeable = EditorGUILayout.Toggle(isUpgradeable, GUILayout.Width(20));
        //    property.FindPropertyRelative("isUpgradeable").boolValue = isUpgradeable;
        //    if (isUpgradeable)
        //    {
        //        //CreateBoolToggle(property, "MinUp", "hasMinUpgradeValue", "minUpgradeValue", 46);
        //        //CreateBoolToggle(property, "MaxUp", "hasMaxUpgradeValue", "maxUpgradeValue", 46);
        //    }
        //    EditorGUILayout.EndHorizontal();
        //}
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var root = new VisualElement();
            var modeProp = new PropertyField(property.FindPropertyRelative("mode"));
            root.Add(modeProp);
            modeProp.RegisterValueChangeCallback(
            (propertyChanged) =>
            {
                root.MarkDirtyRepaint();
                foreach (var child in root.Children())
                {
                    child.MarkDirtyRepaint();
                }
            });

            if ((ValueMode)(property.FindPropertyRelative("mode").enumValueIndex) == ValueMode.Flat)
            {
                root.Add(new PropertyField(property.FindPropertyRelative("flat")));
            }
            else
            {
                root.Add(new PropertyField(property.FindPropertyRelative("min")));
                root.Add(new PropertyField(property.FindPropertyRelative("max")));
            }
            return root;
        }
        public void Callback(EventCallback<SerializedPropertyChangeEvent> args)
        {

        }
    }
}
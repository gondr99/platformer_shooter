using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(DropInfo))]
public class CustomDropInfoDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        float line = EditorGUIUtility.singleLineHeight;
        Rect itemSORect = new Rect(position.x, position.y, position.width , position.height * 0.5f - 2);
        Rect dropRateRect = new Rect(position.x, position.y + line + 4, position.width, position.height * 0.5f - 2);

        SerializedProperty itemSOProp = property.FindPropertyRelative("item");
        SerializedProperty dropRateProp = property.FindPropertyRelative("dropRate");

        EditorGUI.PropertyField(itemSORect, itemSOProp, GUIContent.none); //no title so drawer;
        dropRateProp.floatValue = EditorGUI.Slider(dropRateRect, dropRateProp.floatValue, 0f, 1f);

        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight * 2 + 4; 
        //return base.GetPropertyHeight(property, label);
    }
}

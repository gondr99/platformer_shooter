using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

[CustomEditor(typeof(ItemSO))]
public class CustomItemSO : Editor
{
    private SerializedProperty itemTypeProp;
    private SerializedProperty ammoTypeProp;
    private SerializedProperty minAmountProp, maxAmountProp;
    private SerializedProperty prefabProp;

    private void OnEnable()
    {
        itemTypeProp = serializedObject.FindProperty("itemType");
        ammoTypeProp = serializedObject.FindProperty("ammoType");
        minAmountProp = serializedObject.FindProperty("minAmount");
        maxAmountProp = serializedObject.FindProperty("maxAmount");
        prefabProp = serializedObject.FindProperty("prefab");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(itemTypeProp);

        if (itemTypeProp.GetEnumValue<ItemType>() == ItemType.Ammo)
        {
            EditorGUILayout.PropertyField(ammoTypeProp);
        }


        EditorGUILayout.LabelField("MinMax Value");
        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.PropertyField(minAmountProp, GUIContent.none);
            EditorGUILayout.PropertyField(maxAmountProp, GUIContent.none);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.PropertyField(prefabProp);

        serializedObject.ApplyModifiedProperties();
    }
}


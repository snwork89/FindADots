using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof (DataToSprite))]
[CanEditMultipleObjects]
[System.Serializable]
public class DataToSpriteDrawer : Editor
{
    private ReorderableList mappingDataList;
    private ReorderableList selectedMappingDataList;

    private void OnEnable()
    {
        InitializeReordableList(ref mappingDataList, "mappingDataList", "Mapping Data");
        InitializeReordableList(ref selectedMappingDataList, "selectedMappingDataList", "Selected Mapping Data");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        mappingDataList.DoLayoutList();
        selectedMappingDataList.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }


    private void InitializeReordableList(ref ReorderableList list,string propertyName,string listLabel)
    {
        Color pickColor = Color.black;
        list = new ReorderableList(serializedObject, serializedObject.FindProperty(propertyName), true, true, true, true);
        list.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, listLabel);
        };
        var l = list;
       
        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
          {
              var element = l.serializedProperty.GetArrayElementAtIndex(index); 
              rect.y += 2;
              EditorGUI.PropertyField(
               new Rect(rect.x, rect.y,  60, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("letter"), GUIContent.none);

              EditorGUI.PropertyField(
               new Rect(rect.x+70, rect.y, 90, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("letterImage"), GUIContent.none);

              EditorGUI.PropertyField(
              new Rect(rect.x + 70+100, rect.y, 90, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("letterCompleteImage"), GUIContent.none);


              
              EditorGUI.PropertyField(
                new Rect(rect.x + 70 + 120 + 100, rect.y, 120, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("circleColor"), GUIContent.none);
          };

    }


}

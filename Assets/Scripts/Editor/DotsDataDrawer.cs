using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Text.RegularExpressions;

[CustomEditor(typeof(DotsData), false)]
[CanEditMultipleObjects]
[System.Serializable]

public class DotsDataDrawer : Editor
{
    private DotsData GameDataInstance => target as DotsData;
    private ReorderableList _dataList;
    private ReorderableList startPosition;
    private void OnEnable()
    {
        InitializeReordableList(ref startPosition, "startPosition", "Start Position");
       
    }



    private void InitializeReordableList(ref ReorderableList list, string propertyName, string listLabel)
    {
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
             new Rect(rect.x, rect.y, 60, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("mappingLetter"), GUIContent.none);

            EditorGUI.PropertyField(
             new Rect(rect.x + 70, rect.y, rect.width - 60 - 30, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("initialIndex"), GUIContent.none);
        };

    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        startPosition.DoLayoutList();
        DrawColumnRowsInputField();
        EditorGUILayout.Space();
        if (GameDataInstance.Board != null && GameDataInstance.Columns > 0 && GameDataInstance.Rows > 0)
        {
            DrawBoardTable();
        }

        GUILayout.BeginHorizontal();
        ClearBoardButton();
        FillUpWithRandomLettersButton();
        GUILayout.EndHorizontal();
        serializedObject.ApplyModifiedProperties();
        if (GUI.changed)
        {
            EditorUtility.SetDirty(GameDataInstance);

        }
    }

    private void DrawColumnRowsInputField()
    {
        var columnsTemp = GameDataInstance.Columns;
        var rowsTemp = GameDataInstance.Rows;

        GameDataInstance.Columns = EditorGUILayout.IntField("Columns", GameDataInstance.Columns);
        GameDataInstance.Rows = EditorGUILayout.IntField("Rows", GameDataInstance.Rows);

        if ((GameDataInstance.Columns != columnsTemp || GameDataInstance.Rows != rowsTemp) && GameDataInstance.Columns > 0 && GameDataInstance.Rows > 0)
        {
            GameDataInstance.CreateNewBoard();
        }

    }
    private void DrawBoardTable()
    {
        var tableStyle = new GUIStyle("box");
        tableStyle.padding = new RectOffset(10, 10, 10, 10);
        tableStyle.margin.left = 32;
        var headerColumnStyle = new GUIStyle();
        headerColumnStyle.fixedWidth = 35;
        var columnStyle = new GUIStyle();
        columnStyle.fixedWidth = 50;
        var rowStyle = new GUIStyle();
        rowStyle.fixedHeight = 25;
        rowStyle.fixedWidth = 40;
        rowStyle.alignment = TextAnchor.MiddleCenter;
        var textFieldStyle = new GUIStyle();
        textFieldStyle.normal.background = Texture2D.grayTexture;
        textFieldStyle.normal.textColor = Color.white;
        textFieldStyle.fontStyle = FontStyle.Bold;
        textFieldStyle.alignment = TextAnchor.MiddleCenter;
        EditorGUILayout.BeginHorizontal(tableStyle);
        for (var x = 0; x < GameDataInstance.Columns; x++)
        {
            EditorGUILayout.BeginVertical(x == -1 ? headerColumnStyle : columnStyle);
            for (var y = 0; y < GameDataInstance.Rows; y++)
            {
                if (x >= 0 && y >= 0)
                {
                    EditorGUILayout.BeginHorizontal(rowStyle);
                    var character = (string)EditorGUILayout.TextArea(GameDataInstance.Board[x].Row[y], textFieldStyle);
                    if (GameDataInstance.Board[x].Row[y].Length > 1)
                    {
                        character = GameDataInstance.Board[x].Row[y].Substring(0, 1);

                    }
                    GameDataInstance.Board[x].Row[y] = character;

                    EditorGUILayout.EndHorizontal();
                }
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndHorizontal();
    }


    private void ClearBoardButton()
    {
        if (GUILayout.Button(text: "Clear Board"))
        {
            for (int i = 0; i < GameDataInstance.Columns; i++)
            {
                for (int j = 0; j < GameDataInstance.Rows; j++)
                {
                    GameDataInstance.Board[i].Row[j] = " ";
                }

            }
        }
    }


    private void FillUpWithRandomLettersButton()
    {
        if (GUILayout.Button(text: "Fill Up With Random"))
        {
            for (int i = 0; i < GameDataInstance.Columns; i++)
            {
                for (int j = 0; j < GameDataInstance.Rows; j++)

                {
                    int errorCounter = Regex.Matches(input: GameDataInstance.Board[i].Row[j], pattern: @"[a-zA-Z]").Count;
                    string letters = "10";

                    int index = UnityEngine.Random.Range(0, letters.Length);
                    if (errorCounter == 0)
                    {
                        GameDataInstance.Board[i].Row[j] = letters[index].ToString();
                    }
                }

            }

        }       


    }

         










    }





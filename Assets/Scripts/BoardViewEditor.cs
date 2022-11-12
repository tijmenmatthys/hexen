using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BoardView))]
public class BoardViewEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        //EditorGUILayout.LabelField("test label");

        BoardView boardView = (BoardView)target;
        if (GUILayout.Button("Initialize Board"))
        {
            boardView.ResetBoard();
        }
    }
}

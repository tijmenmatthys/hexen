#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameSystem.Views
{
    // This class adds a button in the inspector for BoardView,
    // which can be used to generate the board in Editor Mode
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
}
#endif

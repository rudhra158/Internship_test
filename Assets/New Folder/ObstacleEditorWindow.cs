using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ObstacleEditorWindow : EditorWindow
{
    private ObstacleData obstacleData;

    [MenuItem("Tools/Obstacle Editor")]
    public static void ShowWindow()
    {
        GetWindow<ObstacleEditorWindow>("Obstacle Editor");
    }

    private void OnGUI()
    {
        GUILayout.Label("Obstacle Grid Editor", EditorStyles.boldLabel);

        obstacleData = (ObstacleData)EditorGUILayout.ObjectField("Obstacle Data", obstacleData, typeof(ObstacleData), false);

        if (obstacleData == null)
        {
            EditorGUILayout.HelpBox("Please assign an ObstacleData Scriptable Object.", MessageType.Warning);
            return;
        }

        // Display a 10x10 grid of toggles
        for (int y = 0; y < 10; y++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int x = 0; x < 10; x++)
            {
                int index = y * 10 + x;
                obstacleData.obstacleGrid[index] = GUILayout.Toggle(obstacleData.obstacleGrid[index], GUIContent.none, GUILayout.Width(20), GUILayout.Height(20));
            }
            EditorGUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Save Changes"))
        {
            EditorUtility.SetDirty(obstacleData);
            AssetDatabase.SaveAssets();
        }
    }
}


using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(LevelBuilderScript))]
public class LevelBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LevelBuilderScript myScript = (LevelBuilderScript)target;
        if (GUILayout.Button("Build Level"))
        {
            myScript.BuildLevel();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapGeneration))]
public class GenerationMaps : Editor
{
    public override void OnInspectorGUI()
    {
        MapGeneration generationMaps = (MapGeneration)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Generate")||generationMaps.GenerationAllTime)
        {
            generationMaps.GenerateMap();
        }
    }
}

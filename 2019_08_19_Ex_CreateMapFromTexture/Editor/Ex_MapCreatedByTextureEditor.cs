using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Ex_MapCreatedByTexture))]
public class Ex_MapCreatedByTextureEditor : Editor
{
 
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Create Map")) {
            Ex_MapCreatedByTexture myScript = (Ex_MapCreatedByTexture)target;
            myScript.CreateMap();
        }
    }
}
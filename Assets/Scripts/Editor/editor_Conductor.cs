using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Conductor))]
public class editor_Conductor : Editor 
{

    public override void OnInspectorGUI () 
    {
        DrawDefaultInspector();
        if(GUILayout.Button("Is beat valid?")) 
        {
            bool valid = Conductor._instance.CheckValidBeat();
            Debug.Log("Valid? "+valid);
        }
    }
}

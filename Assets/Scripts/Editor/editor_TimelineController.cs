using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TimelineController))]
public class editor_TimelineController : Editor 
{
    public override void OnInspectorGUI () 
    {
        DrawDefaultInspector();
        // if(GUILayout.Button("Move right")) 
        // {
        //     // bool valid = Conductor._instance.CheckValidBeat();
        //     // Debug.Log("Valid? "+valid);
        //     PlayerTimelineController controller = Selection.activeGameObject.GetComponent<PlayerTimelineController>();
        //     controller.TryMoveRight();
        // }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerTimelineController))]
public class editor_PlayerTimelineController : Editor 
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

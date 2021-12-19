using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Conductor))]
 // ^ This is the script we are making a custom editor for.
public class editor_Conductor : Editor {

    public override void OnInspectorGUI () {
    //Called whenever the inspector is drawn for this object.
        DrawDefaultInspector();
        //This draws the default screen.  You don't need this if you want
        //to start from scratch, but I use this when I'm just adding a button or
        //some small addition and don't feel like recreating the whole inspector.
        if(GUILayout.Button("Is beat valid?")) {
            //add everthing the button would do.
            bool valid = Conductor.Instance.CheckValidBeat();
            Debug.Log("Valid? "+valid);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Lighter))]
public class LighterEditorAdds : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Зажечь"))
        {
            ((Lighter)target).LightTheLighter();
        }
    }
}

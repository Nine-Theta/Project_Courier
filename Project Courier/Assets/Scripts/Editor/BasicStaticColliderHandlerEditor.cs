using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BasicStaticColliderHandler))]
public class BasicStaticColliderHandlerEditor : Editor
{
    bool North = false;
    bool East = false;
    bool South = false;
    bool West = false;

    public override void OnInspectorGUI()
    {
        BasicStaticColliderHandler handler = (BasicStaticColliderHandler)target;

        float buttonWidth = 56;
        float buttonheight = 22;
        float buttonOffset = 80;

        GUILayout.Space(4);

        GUILayout.BeginHorizontal();
        GUILayout.Space(buttonOffset + buttonWidth * 1.6f);
        handler.DirectionBoxes[0] = GUILayout.Toggle(handler.DirectionBoxes[0], "North", GUILayout.Width(buttonWidth), GUILayout.Height(buttonheight));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("test", GUILayout.Width(buttonWidth), GUILayout.Height(buttonheight));
        GUILayout.Space(buttonOffset);
        West = GUILayout.Toggle(handler.DirectionBoxes[3], "West", GUILayout.Width(buttonWidth), GUILayout.Height(buttonheight));
        handler.DirectionBoxes[3] = West;

        East = GUILayout.Toggle(handler.DirectionBoxes[1], "East", GUILayout.Width(buttonWidth), GUILayout.Height(buttonheight));
        handler.DirectionBoxes[1] = East;
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Space(buttonOffset + buttonWidth * 1.6f);
        South = GUILayout.Toggle(handler.DirectionBoxes[2], "South", GUILayout.Width(buttonWidth*2), GUILayout.Height(buttonheight));
        handler.DirectionBoxes[2] = South;
        GUILayout.EndHorizontal();

        GUILayout.Space(4);

        base.DrawDefaultInspector();
    }
}

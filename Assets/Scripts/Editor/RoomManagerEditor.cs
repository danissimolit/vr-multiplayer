using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RoomManager))]
public class RoomManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.HelpBox("This script is responsible for creating and joining Rooms.", MessageType.Info);

        RoomManager room = (RoomManager)target;

        if (GUILayout.Button("Join School Room"))
        {
            room.OnEnterButtonClicked_School();
        }

        if (GUILayout.Button("Join Outdoor Room"))
        {
            room.OnEnterButtonClicked_Outdoor();
        }
    }
}

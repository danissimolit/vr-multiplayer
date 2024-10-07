using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LoginManager))]
public class LoginManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.HelpBox("This script is responsible for connecting to Photon Servers.", MessageType.Info);

        LoginManager login = (LoginManager)target;

        if (GUILayout.Button("Connect to Photon Servers Anonymously"))
        {
            login.ConnectAnonymously();
        }
    }
}

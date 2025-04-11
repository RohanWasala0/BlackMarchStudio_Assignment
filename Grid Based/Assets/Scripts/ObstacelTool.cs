using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering.VirtualTexturing;

public class ObstacelTool : EditorWindow
{
    private ObstacelData _obstacelData;
    [MenuItem("Grid/ObstacelTool/ShowWindow")]
    static void ShowWindow(){
        GetWindow<ObstacelTool>("Obstacel Editor");
    }

    private void OnGUI(){
        GUILayout.Label("Obstacel Tool", EditorStyles.boldLabel);
        _obstacelData = (ObstacelData)EditorGUILayout.ObjectField("Obstacel Data", _obstacelData, typeof(ObstacelData), false);

        if (_obstacelData==null){
            EditorGUILayout.HelpBox("Assign a Obstacle Data Scriptable Object to edit.", MessageType.Info);
            return;
        }

        EditorGUILayout.Space();
        for(int y=0; y<10; y++){
            EditorGUILayout.BeginHorizontal();
            for(int x=0; x<10; x++){
                _obstacelData.Set(x, y, GUILayout.Toggle(_obstacelData.Get(x, y), "", GUILayout.Width(20), GUILayout.Height(20)));
            }
            EditorGUILayout.EndHorizontal();
        }
        if(GUI.changed){EditorUtility.SetDirty(_obstacelData);}
    }
}

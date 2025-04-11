using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(GridManager))]
public class GridGenerator : Editor{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GridManager gridManager = (GridManager)target;
        if (GUILayout.Button("Generate Grid")){gridManager.GenerateGrid();}
        if (GUILayout.Button("Generate Obstacles")){gridManager.GenerateObstacle();}

        GUILayout.Label("Obstacel Tool", EditorStyles.boldLabel);

        EditorGUILayout.Space();
        for(int y=0; y<10; y++){
            EditorGUILayout.BeginHorizontal();
            for(int x=0; x<10; x++){
                gridManager._obstacelData.Set(x, y, GUILayout.Toggle(gridManager._obstacelData.Get(x, y), "", GUILayout.Width(20), GUILayout.Height(20)));
            }
            EditorGUILayout.EndHorizontal();
        }
        if(GUI.changed){EditorUtility.SetDirty(gridManager._obstacelData);}
    }
}
#endif
public class GridManager : MonoBehaviour
{
    //Grid Size 10x10
    public int width = 10;
    public int height = 10;
    public float spacing = 1f;
    public List<Cell> db_cells;
    [SerializeField] public ObstacelData _obstacelData;
    [SerializeField] private GameObject _obstaclePrefab;
    [SerializeField] private GameObject _cellPrefab;
    [SerializeField] private ObstacleManager obstacleManager;
    // Start is called before the first frame update
    public void GenerateGrid(){
        foreach(Transform child in transform){
            if(child.CompareTag("Cell")){DestroyImmediate(child.gameObject);}
        }

        for(int x=0; x < width; x++){
            for(int y=0; y < height; y++){
                Vector3 position = new Vector3(x* spacing, 0, y* spacing);
                GameObject cell = Instantiate(_cellPrefab, position, Quaternion.identity, transform);
                cell.name = $"Cell_({x},{y})";
            }
        }
    }
    public void GenerateObstacle(){
        foreach(Transform child in transform){
            if(child.CompareTag("Cell") && child.childCount > 0){
                foreach(Transform obstacle in child){
                    if(obstacle.CompareTag("Obstacle")){DestroyImmediate(obstacle.gameObject);}
                }
            }
        }
        obstacleManager.GenerateObstacle(_obstacelData, _obstaclePrefab, gameObject);
    }

}

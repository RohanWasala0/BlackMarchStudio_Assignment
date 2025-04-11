using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public void GenerateObstacle(ObstacelData _obstacelData, GameObject _obstaclePrefab, GameObject _gridManager){
        for(int x=0; x<10; x++){
            for(int y=0; y<10; y++){
                if (_obstacelData.Get(x, y)){
                    Transform cell = _gridManager.transform.Find($"Cell_({x},{y})"); 
                    if (cell != null){
                        Cell cell_info = cell.GetComponent<Cell>();
                        cell_info.hasObstacle = true;
                        Vector3 position = new Vector3(cell.position.x, 1, cell.position.z);
                        GameObject obstacle = Instantiate(_obstaclePrefab, position, Quaternion.identity, cell);
                        obstacle.name = $"Obstacle Cell({x},{y})";
                    }
                    else{Debug.Log($"Cell_({x},{y}) not found");}
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

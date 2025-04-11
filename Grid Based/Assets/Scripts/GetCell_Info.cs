using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class GetCell_Info : MonoBehaviour
{
    private Camera _Camera;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private PathFinder pathFinder;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GridManager gridManager;

    void Awake()
    {
        _Camera = GetComponentInChildren<Camera>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = _Camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)){
            if(hit.collider.CompareTag("Cell")){
                Cell _cell = hit.collider.GetComponent<Cell>();
                _cell.hovering = true;
                infoText.text = _cell != null ? $"Cell Position: ({_cell.transform.position}) \n Has Obstacle: {_cell.hasObstacle}" : ""; 
                List<Vector3> path = pathFinder.FindPath(playerController.transform.position, _cell.transform.position);
                foreach(Vector3 point in path){
                    foreach(Transform child in gridManager.transform){
                        if(child.position == point){
                            Cell temp_cell = child.GetComponent<Cell>();
                            temp_cell.hovering = true;
                        }
                    }
                }
                if(Input.GetMouseButtonDown(0)){
                    playerController.MoveToPosition(hit.point);
                    // foreach(Vector3 point in path){Debug.Log(point);}    
                }
            }
        }
        else{
            infoText.text = "";
        }
        
    }
}

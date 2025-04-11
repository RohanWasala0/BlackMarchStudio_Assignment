using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isMoving = false;
    [SerializeField] private float moveSpeed = 5f;
    private PathFinder pathFinder;
    private List<Vector3> currentPath = new List<Vector3>();

    private void Start() {
        pathFinder = FindAnyObjectByType<PathFinder>();
    }
    private void Update(){
        if(isMoving){return;}

        if(Input.GetMouseButtonDown(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit)){
                if(hit.collider.CompareTag("Cell")){MoveToPosition(hit.collider.transform.position);}
            }
        }
    }
    public void MoveToPosition(Vector3 targetPosition){
        Vector3 targetPos = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
        currentPath = pathFinder.FindPath(transform.position, targetPos);
        if(currentPath.Count>0){StartCoroutine(FollowPath());}
    }
    private IEnumerator FollowPath(){
        isMoving = true;

        foreach(Vector3 waypoint in currentPath){
            while(Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(waypoint.x, waypoint.z)) > 0.1f){
                Debug.Log(waypoint);
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    new Vector3(waypoint.x, transform.position.y, waypoint.z),
                    moveSpeed * Time.deltaTime
                );
                yield return null;
            }
        }
        isMoving = false;
    }
}

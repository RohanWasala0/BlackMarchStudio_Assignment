using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    private GridManager gridManager;
    private void Awake(){gridManager = GetComponent<GridManager>();}

    public List<Vector3> FindPath(Vector3 startPosition, Vector3 endPosition){
        Vector2Int start = WorlToGrid(startPosition);
        Vector2Int end = WorlToGrid(endPosition);

        if(!IsValid(end)){return new List<Vector3>();}

        Dictionary<Vector2Int, Vector2Int> cameFrom = new Dictionary<Vector2Int, Vector2Int>();
        Dictionary<Vector2Int, int> gCost = new Dictionary<Vector2Int, int>();
        Dictionary<Vector2Int, int> fCost = new Dictionary<Vector2Int, int>();
        HashSet<Vector2Int> openPosition = new HashSet<Vector2Int>();
        HashSet<Vector2Int> closedPosition = new HashSet<Vector2Int>();

        openPosition.Add(start);
        gCost[start] = 0;
        fCost[start] = HusristicDistance(start, end);

        //Main Loop
        while(openPosition.Count > 0){
            Vector2Int current = GetLowestFCost(openPosition, fCost);
            if(current == end){return ReconstructPath(cameFrom, start, end, endPosition);}

            openPosition.Remove(current);
            closedPosition.Add(current);

            foreach(Vector2Int direction in new Vector2Int[]{
                Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left
            }){
                Vector2Int neighbor = current + direction;
                if(!IsValid(neighbor) || closedPosition.Contains(neighbor)){continue;}

                int temp_gCost = gCost[current] +1;
                if(!openPosition.Contains(neighbor)){openPosition.Add(neighbor);}
                else if(temp_gCost >= (gCost.ContainsKey(neighbor) ? gCost[neighbor] : int.MaxValue)){continue;}

                cameFrom[neighbor] = current;
                gCost[neighbor] = temp_gCost;
                fCost[neighbor] = temp_gCost + HusristicDistance(neighbor, end);
            }
        }

        return new List<Vector3>();   
    }
    private Vector2Int WorlToGrid(Vector3 worldPosition){
        return new Vector2Int(
            Mathf.RoundToInt(worldPosition.x/ gridManager.spacing),
            Mathf.RoundToInt(worldPosition.z/ gridManager.spacing)
        );
    }
    private Vector3 GridToWorld(Vector2Int gridPosition){
        return new Vector3(
            gridPosition.x * gridManager.spacing, 0,
            gridPosition.y * gridManager.spacing
        );
    }
    private bool IsValid(Vector2Int gridPosition){
        if (gridPosition.x < 0 || gridPosition.x >= gridManager.width || gridPosition.y < 0 || gridPosition.y >= gridManager.height) {
            return false;
        }
        return !gridManager._obstacelData.Get(gridPosition.x, gridPosition.y);
    } 
    private int HusristicDistance(Vector2Int a, Vector2Int b){
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }
    private Vector2Int GetLowestFCost(HashSet<Vector2Int> openPosition, Dictionary<Vector2Int, int> fCost){
        Vector2Int lowest = Vector2Int.zero;
        int lowestCost = int.MaxValue;

        foreach(Vector2Int position in openPosition){
            int cost = fCost.ContainsKey(position) ? fCost[position] : int.MaxValue;
            if(cost < lowestCost){
                lowestCost = cost;
                lowest = position;
            }
        }
        return lowest;
    }
    private List<Vector3> ReconstructPath(Dictionary<Vector2Int, Vector2Int> cameFrom, Vector2Int start, Vector2Int end, Vector3 endPosition){
        List<Vector3> path = new List<Vector3>();
        Vector2Int current = end;

        while(current != start){
            path.Add(GridToWorld(current));
            current = cameFrom[current];
        }

        path.Reverse();
        if(path.Count > 0){path[path.Count -1] = endPosition;}
        return path;
    } 
}
// Referenced 
// sebastian lague pathfinding 
// Youtube https://www.youtube.com/watch?v=-L-WgKMFuhE&list=PLFt_AvWsXl0cq5Umv3pMC9SPnKjfp9eGW
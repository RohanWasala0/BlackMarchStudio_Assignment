using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public int x;
    public int y;
    public bool hasObstacle;
    public bool hovering = false;
    private Material material;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;        
    }

    // Update is called once per frame
    void Update()
    {
        if (hovering){
            material.SetColor("_Color", Color.white);
            hovering = false;
        }        
        else{
            material.SetColor("_Color", Color.green);
        }
    }
}

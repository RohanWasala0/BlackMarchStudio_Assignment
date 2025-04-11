using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SO_ObstacelData", menuName = "Grid/Obstacel Data")]
public class ObstacelData : ScriptableObject
{
    public bool[] obsData = new bool[100];

    public void Set(int x, int y, bool value){
        obsData[y*10 + x] = value;
    }
    
    public bool Get(int x, int y){
        //Debug.Log($"{x},{y}");
        return obsData[y*10 + x];
    }
}

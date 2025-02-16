using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DropData
{
    public List<string> items = new List<string>();
    public List<Vector3> positions = new List<Vector3>();
    public int nextID;
    public DropData()
    {
        items.Clear();
        positions.Clear();
        nextID = 0;
    }
    public void Add(string item, Vector3 position)
    {
        items.Add(item);
        positions.Add(position);
    }

}

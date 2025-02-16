using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class BlockChange
{
    public List<string> changeCodes = new List<string>();
    public bool exposed;

    public int FindChangeToBlock(Vector3 position)
    {
        string identifier = position.x + "/" + position.y + "/" + position.z;
        for (int i = 0; i < changeCodes.Count; i++)
        {
            if (changeCodes[i].Contains(identifier))
            {
                return i;
            }
        }
        return -1;
    }
}

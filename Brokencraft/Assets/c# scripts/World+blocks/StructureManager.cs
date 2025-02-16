using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureManager : MonoBehaviour
{
    [SerializeField] private TextAsset[] structures;
    private static StructureManager instance;
    public void Start()
    {
        instance = this;
    }
    public static BlockChange GetStructure(string name)
    {
        foreach(TextAsset structure in instance.structures)
        {
            if(structure.name == name)
            {
                return JsonUtility.FromJson<BlockChange>(structure.text);
            }
        }
        return null;
    }
}

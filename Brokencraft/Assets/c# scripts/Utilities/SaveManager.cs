using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public delegate void Save();
    private static List<Save> saveCommands = new List<Save>();
    // Start is called before the first frame update
    public static  void SaveAll(bool flush)
    {
        foreach(Save save in saveCommands)
        {
            save();
        }
        if(flush)
        {
            saveCommands = new List<Save>();
        }
    }
    public static  void Register(Save saver)
    {
        saveCommands.Add(saver);
    }
    void OnApplicationQuit()
    {
       SaveAll(false);
    }
}

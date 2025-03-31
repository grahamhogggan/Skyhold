using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class PersistentDatahandler : MonoBehaviour
{
    private string pDataPath;
    // Start is called before the first frame update
    void Awake()
    {
        Overlord.saveDirectory = Application.persistentDataPath; // ifmodifying this, also note GameStarter.cs has its own field fo rreasons.
        pDataPath = Overlord.saveDirectory + "/pData.save";
        SaveManager.Register(SavePersistentData);
        ReadPersistentData();
    }
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
    }
    void ReadPersistentData()
    {
        if (File.Exists(pDataPath))
        {
            persistentDataStruct pData = JsonUtility.FromJson<persistentDataStruct>(File.ReadAllText(pDataPath));
            PersistentData.Init(pData);
        }
        else
        {
            PersistentData.Init(9);
        }
    }
    void SavePersistentData()
    {
        string dataSav = JsonUtility.ToJson(PersistentData.getSaveData());
        File.WriteAllText(pDataPath, dataSav);
    }
    public void OnDestroy()
    {
        SavePersistentData();
    }
}

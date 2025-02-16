using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class Extracter : MonoBehaviour
{
    private GameObject generating;
    public string itemGenerating;
    public float delay;
    public float spawnTime;
    [SerializeField] private GameObject indicator;
    private Vector3 indicatorPosition;
    private float indicatorY;
    private string keyCode;
    private string dataPath;
    private ExtracterData data;

    [SerializeField] private GameObject dispLoc;
    private GameObject dispMesh;
    // Start is called before the first frame update
    void Start()
    {
        data = new ExtracterData();
        delay = 0;
        itemGenerating = "empty";
        indicatorPosition = indicator.transform.position;
        indicatorY = indicator.transform.localScale.y;
        keyCode = "EXTR_" + (int)transform.position.x + (int)transform.position.y + (int)transform.position.z;
        Directory.CreateDirectory(Application.persistentDataPath+"/Extracters");
        Load();
        UpdateDisp();
    }

    // Update is called once per frame
    void Update()
    {
        if(itemGenerating!="empty")
        {
            delay += Time.deltaTime;
            if (delay > spawnTime)
            {
                delay = 0;
                Overlord.Drop(itemGenerating, transform.position + new Vector3(0, 1, 0));
                ScoreManager.AddScore(1);
            }
            indicator.transform.localScale = new Vector3(indicator.transform.localScale.x, indicatorY * delay / spawnTime, indicator.transform.localScale.z);
            indicator.transform.position = indicatorPosition + indicator.transform.up * (-indicatorY / 2 + indicator.transform.localScale.y / 2);
        }
        Save();
    }
    public void SetItem()
    {
        itemGenerating = Miner.GetEquippedItemName();
        generating = Miner.GetBlock(itemGenerating);
        spawnTime = generating.GetComponentInChildren<BlockDetails>().value * 10;
                UpdateDisp();

    }
    void Load()
    {
        dataPath = Application.persistentDataPath + "/Extracters/" + keyCode;
        if(File.Exists(dataPath)&&!Overlord.OverwriteMode)
        {
            string read = File.ReadAllText(dataPath);
            data = JsonUtility.FromJson<ExtracterData>(read);
            itemGenerating = data.itemGenerating;
            delay = data.delay;
            spawnTime = data.spawnTime;
        }
    }
    void Save()
    {
        data.itemGenerating = itemGenerating;
        data.delay = delay;
        data.spawnTime = spawnTime;
        string saveData = JsonUtility.ToJson(data);
        File.WriteAllText(dataPath, saveData);
    }
    void UpdateDisp()
    {
        GameObject bloc = Miner.GetBlock(itemGenerating);
        if(bloc==null) return;
        GameObject disp = bloc.GetComponentInChildren<MeshUnifier>().meshRoot;

        Destroy(dispMesh);
        dispMesh = Instantiate(disp,dispLoc.transform.position,Quaternion.identity);
        dispMesh.transform.localScale /=5;
        dispMesh.transform.SetParent(dispLoc.transform);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Overlord : MonoBehaviour
{
    public static bool paused = false;
    public static bool OverwriteMode = false;
    public float secondsPerDay;
    public float secondsPerNight;
    public Light sun;
    private bool isNight;
    public static Overlord instance;
    [SerializeField] public GameObject dropPrefab;
    private string DropSavePath;
    public DropData dropData;

    public static Player player;
    void Awake()
    {
    instance = this;
        DropSavePath = Application.persistentDataPath + "/drops.randomFileExtension";
        ReadDrops();
    }
    public static void Pause()
    {
        Time.timeScale = 0;
        MouseLook.active = false;
        paused = true;
    }
    public static void Resume()
    {
        Time.timeScale = 1;
        MouseLook.active = true;
        paused=false;
    }
    void Update()
    {
        if (!paused)
        {
            if(sun.transform.rotation.eulerAngles.x>180)
            {
                sun.transform.Rotate(360 / secondsPerNight * Time.deltaTime, 0, 0);
                isNight = true;
                if (sun.intensity > 0)
                {
                    sun.intensity -= Time.deltaTime;
                }
            }
            else
            {
                sun.transform.Rotate(360 / secondsPerDay * Time.deltaTime, 0, 0);
                isNight=false;
                if(sun.intensity<2)
                {
                    sun.intensity += Time.deltaTime;
                }
            }

            
        }
        SaveDrops();
    }
    public static bool IsNight() { return instance.isNight; }
    public static float GetRawTime()
    {

        return instance.sun.transform.rotation.eulerAngles.x;
    }
    public static void SetRawTime(float timeTo)
    {
        
        instance.sun.transform.rotation = Quaternion.Euler(timeTo,instance.sun.transform.eulerAngles.y,instance.sun.transform.eulerAngles.z);

    }
    public static void Drop(string item,Vector3 position)
    {
        DroppedItem d = Instantiate(instance.dropPrefab, position, Quaternion.identity).GetComponent<DroppedItem>();
        d.item = item;
        d.ID = instance.dropData.nextID;
        instance.dropData.nextID++;
        instance.dropData.items.Add(item);
        instance.dropData.positions.Add(position);
        instance.SaveDrops();
    }
    public static void DeleteDrop(int ID)
    {
        instance.dropData.items[ID] = "deleted";
    }
    private void SaveDrops()
    {
        string data = JsonUtility.ToJson(dropData);
        File.WriteAllText(DropSavePath,data);
    }
    private void ReadDrops()
    {
        if(File.Exists(DropSavePath)&&!OverwriteMode)
        {
            string data = File.ReadAllText(DropSavePath); 
            dropData = JsonUtility.FromJson<DropData> (data);
            for(int i = 0; i < dropData.items.Count; i++)
            {
                if (dropData.items[i]!="deleted")
                {
                    DroppedItem d = Instantiate(dropPrefab, dropData.positions[i], Quaternion.identity).GetComponent<DroppedItem>();
                    d.item = dropData.items[i];
                    d.ID = i;
                }

            }
        }
        else
        {
            dropData = new DropData();

        }
    }
}

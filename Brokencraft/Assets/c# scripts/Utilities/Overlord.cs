using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class Overlord : MonoBehaviour
{

    public static bool paused = false;
    public static bool inInventory = false;
    public static bool OverwriteMode = false;
    [Header("Day/Night Cycle")]
    public float secondsPerDay;
    public float secondsPerNight;

    public float minSecondsPerDay;
    public Light sun;
    private bool isNight;
    public static Overlord instance;
    [Header("Other")]
    public static int difficulty = -1;
    [SerializeField] public GameObject dropPrefab;

    private string DropSavePath;
    public DropData dropData;
    private string infoSavePath;
    public static string saveDirectory;
    public static Player player;

    private bool wasNight;
    public int saveCount { get; set; }
    public static int nightCount = 0;
    [SerializeField] private FadingPanel nightCountText;



    public enum WorldType
    {
        Countryside = 0,
        Mountain = 1,
        Desert = 2,
        Snow = 3,
        Jungle = 4,
        Titan = 5,
        Mixed = 6, 
        Space = 7,
        Finale = 8
    }
    public static WorldType type;
    void Awake()
    {
        Overlord.saveDirectory = Application.persistentDataPath; // if modifying this, also note GameStarter.cs has its own field for reasons.
        difficulty = -1;
        Debug.Log("initialized directory");
        instance = this;
        DropSavePath = saveDirectory + "/drops.randomFileExtension";
        infoSavePath = saveDirectory + "/gameInfo.potato";
        nightCount = 0;
        ReadDrops();
        ReadInfo();
        saveCount = 0;
        SaveManager.Register(Save);
        wasNight = true;

    }

    public static void Pause(bool freeze = true)
    {
        Time.timeScale = freeze?0:1;
        MouseLook.active = false;
        FirstPersonMovement.active = false;
        paused = freeze;
        inInventory = true;
    }
    public static void Resume()
    {
        Time.timeScale = 1;
        MouseLook.active = true;
        FirstPersonMovement.active = true;
        paused = false;
        inInventory = false;
    }
    void Update()
    {
        if (!paused)
        {
            if (sun.transform.rotation.eulerAngles.x > 180)
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
                isNight = false;
                if (sun.intensity < 2)
                {
                    sun.intensity += Time.deltaTime;
                }
            }

        if (isNight && !wasNight)
        {
            OnNightStart();
        }
        wasNight = isNight;

        }

    }
    public static bool IsNight() {
        if(type == WorldType.Space) return true;
         return instance.isNight; 
         }
    public static float GetRawTime()
    {

        return instance.sun.transform.rotation.eulerAngles.x;
    }
    public static Vector3 GetRawTimeVector()
    {
        return instance.sun.transform.rotation.eulerAngles;
    }
    public static void SetRawTime(Vector3 timeTo)
    {

        instance.sun.transform.rotation = Quaternion.Euler(timeTo);

    }
    public static void Drop(string item, Vector3 position)
    {
        DroppedItem d = Instantiate(instance.dropPrefab, position, Quaternion.identity).GetComponent<DroppedItem>();
        d.item = item;
        d.ID = instance.dropData.nextID;
        instance.dropData.nextID++;
        instance.dropData.items.Add(item);
        instance.dropData.positions.Add(position);
        instance.Save();
    }
    public static void DeleteDrop(int ID)
    {
        instance.dropData.items[ID] = "deleted";
    }
    public void Save()
    {
        string data = JsonUtility.ToJson(dropData);
        File.WriteAllText(DropSavePath, data);
        string thisData = JsonUtility.ToJson(new GameInformation(this));
        File.WriteAllText(infoSavePath, thisData);
    }
    private void ReadDrops()
    {
        if (File.Exists(DropSavePath) && !OverwriteMode)
        {
            string data = File.ReadAllText(DropSavePath);
            dropData = JsonUtility.FromJson<DropData>(data);
            for (int i = 0; i < dropData.items.Count; i++)
            {
                if (dropData.items[i] != "deleted")
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
    private void ReadInfo()
    {
        if (File.Exists(infoSavePath) && !OverwriteMode)
        {
            string data = File.ReadAllText(infoSavePath);
            GameInformation info = JsonUtility.FromJson<GameInformation>(data);
            this.secondsPerDay = info.secondsPerDay;
            this.secondsPerNight = info.secondsPerNight;
            this.minSecondsPerDay = info.minSecondsPerDay;
            difficulty = info.difficulty;
            type = info.type;
            nightCount = info.nightCount;
        }
    }
    private void OnNightStart()
    {
        Debug.Log("night started");
        if (secondsPerDay > minSecondsPerDay)
        {
            secondsPerDay -= 30;
            secondsPerNight += 30;
        }
        difficulty++;
        nightCount++;
        nightCountText.Show("Night: "+nightCount);
    }

}

[System.Serializable]
class GameInformation
{
    public float secondsPerDay;
    public float secondsPerNight;
    public float minSecondsPerDay;
    public int difficulty;
    public int nightCount;

    public Overlord.WorldType type;
    public GameInformation(Overlord o)
    {
        this.secondsPerDay = o.secondsPerDay;
        this.secondsPerNight = o.secondsPerNight;
        this.minSecondsPerDay = o.minSecondsPerDay;
        this.difficulty = Overlord.difficulty;
        this.type = Overlord.type;
        this.nightCount = Overlord.nightCount;
    }
}
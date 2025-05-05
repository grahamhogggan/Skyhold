using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ResourceUser : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private List<string> accepted;
    [SerializeField] private MonoBehaviour[] toDo;
    [SerializeField] private AudioSource[] toPlay;
    [SerializeField] private GameObject[] indicators;
    [SerializeField] private GameObject[] show;
    private int maxItems;
    private List<string> stored;
    [SerializeField] private float useRate;
    private float delay;
    private string savePath;
    private string keyCode;
        private bool savedAlready = false;

    void Start()
    {
        
        savePath = Overlord.saveDirectory+"/ResourceUsers";
        keyCode = "RES_USE" + (int)transform.position.x + (int)transform.position.y + (int)transform.position.z;
        Directory.CreateDirectory(savePath);
        savePath+="/"+keyCode;
        //load
        Load();
        maxItems = indicators.Length;
        Refresh();

        SaveManager.Register(Save);
        savedAlready = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(stored.Count>0)
        {
        delay+=Time.deltaTime;
        if(delay>useRate)
        {
            delay = 0;
            stored.RemoveAt(0);
            Refresh();
        }
        }

    }
    public void AddTo()
    {
        string item =Miner.GetEquippedItemName();
        if(accepted.Contains(item)&&stored.Count<maxItems)
        {
            stored.Add(item);
            PlayerInventory.StaticRemoveItem(item);
            Refresh();
        }
    }
    public bool AddManually(string item)
    {
        if(accepted.Contains(item)&&stored.Count<maxItems)
        {
            stored.Add(item);
            Refresh();
            return true;
        }
        return false;
    }
    private void Refresh()
    {
        for(int i = 0; i<indicators.Length; i++)
        {
            if(i<stored.Count)
            {
                indicators[i].SetActive(true);
            }
            else
            {
                indicators[i].SetActive(false);
            }
        }
        foreach(GameObject thing in show)
        {
          thing.SetActive(stored.Count>0);
        }
        foreach(MonoBehaviour behaviour in toDo)
        {
            behaviour.enabled = stored.Count>0;
        }
        foreach(AudioSource src in toPlay)
        {
            if(stored.Count>0)
            {
                src.Stop();
            }
            else
            {
                src.Play();
            }
        }
    }
    void Save()
    {
        ResourceUserData data = new ResourceUserData();
        data.stored = this.stored;
        data.delay = this.delay;
        string jsonData = JsonUtility.ToJson(data);
        jsonData = Encryption.Encrypt(jsonData);
        File.WriteAllText(savePath,jsonData);
                savedAlready = true;

    }
    void Load()
    {
        if(File.Exists(savePath))
        {
            ResourceUserData data = JsonUtility.FromJson<ResourceUserData>(Encryption.Decrypt(File.ReadAllText(savePath)));
            delay = data.delay;
            stored = data.stored;

        }
        else
        {
            delay = 0;
            stored = new List<string>();
        }
        
    }
        void OnDisable()
    {
        SaveManager.Withdraw(Save);
        if(File.Exists(savePath)&&!savedAlready)
        {
        File.Delete(savePath);

        }
    }
}
[System.Serializable]
class ResourceUserData
{
    public List<string> stored;
    public float delay;
}

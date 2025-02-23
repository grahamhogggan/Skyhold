using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour,PeriodicSaver
{
    [SerializeField] private int health;
    private int maxHealth;
    public static string dataPath;
    private PlayerData data;
    [SerializeField] private int minY;

    [SerializeField] private HitEffect hitEffect;
    public int saveCount{get;set;}
    // Start is called before the first frame update
    void Start()
    {
        dataPath = Application.persistentDataPath + "/PlayerData.savedata";
        maxHealth = health;
        Load();
        
    }

    // Update is called once per frame
    void Update()
    {
        ((PeriodicSaver)this).TrySave();
        if(health <=0)
        {
            SceneManager.LoadScene(1);
        }
        if(transform.position.y<minY)
        {
            health = -1;
        }
    }
    public void Damage(int damage)
    {
        health -= damage;
        hitEffect.Hit();
        Save();
    }
    public void Heal(int amount)
    {
        health+=amount;
        if(health>maxHealth)
        {
            health = maxHealth;
        }
    }
    public int GetHealth()
    {return health; }
    public void Save()
    {
        data.position = transform.position;
        data.health = health;
        data.time = Overlord.GetRawTime();
        string ToSave = JsonUtility.ToJson(data);
        File.WriteAllText(dataPath, ToSave);
    }
    void Load()
    {
        if (File.Exists(dataPath) && !Overlord.OverwriteMode)
        {
            string loaded = File.ReadAllText(dataPath);
            PlayerData data = JsonUtility.FromJson<PlayerData>(loaded);
            GetComponent<CharacterController>().enabled = false;
            transform.position = data.position;
            GetComponent<CharacterController>().enabled = true;

            health = data.health;
            Overlord.SetRawTime(data.time);
            this.data = data;
        }
        else
        {
            this.data = new PlayerData();
        }
    }
}

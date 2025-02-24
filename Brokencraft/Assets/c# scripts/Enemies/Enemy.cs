using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Enemy : MonoBehaviour,PeriodicSaver
{
    [SerializeField]protected float health = 10;
    public static GameObject target;
    private Renderer renderer;
    private Color initColor;
    private string dataPath;
    public bool DayDeath = true;
    private float burnClock = 0;
    [SerializeField] private string[] drops;

    public int saveCount{get;set;}
    // Start is called before the first frame update
    protected void Start()
    {
        renderer = GetComponentInChildren<Renderer>();
        initColor = renderer.material.color;
        dataPath = Application.persistentDataPath + "/enemies";
        Directory.CreateDirectory(dataPath);
        dataPath += "/enemy_" + EnemySpawner.enemyNumber+".enemyData";
        EnemySpawner.enemyNumber++;
    }

    // Update is called once per frame
    protected void Update()
    {
        if (health <= 0)
        {
            Die();
        }
        if(((Vector4)(initColor - renderer.material.color)).magnitude<0.1)
        {
            renderer.material.color = initColor;
        }
        else
        {
            renderer.material.color += (Color)((Vector4)(initColor - renderer.material.color)).normalized * 3 * Time.deltaTime;

        }
        if(!Overlord.IsNight())
        {
            burnClock += Time.deltaTime;
            if (DayDeath && burnClock > 2f)
            {
                Damage(1);
                burnClock = 0;
            }
        }
        ((PeriodicSaver)this).TrySave();
    }
    public void Damage(float damage)
    {
        health -= damage;
        renderer.material.color = Color.red;
        Save();
    }
    public void PlayerDamage(float damage)
    {
        ScoreManager.AddScore((int)damage);
        Damage(damage);
    }
    public void Die()
    {
        if(Overlord.IsNight())
        {
            Dropstuff();

        }

        Destroy(gameObject);
    }
    private void Dropstuff()
    {
        for(int y = 0; y<drops.Length;  y++)
        {
            Overlord.Drop(drops[y],transform.position+new Vector3(Random.Range(-0.1f,0.1f),y,Random.Range(-0.1f,0.1f)));
            if(Random.Range(0.0f,1f)<0.5f)
            {
                return;
            }
        }
    }
    public void Save()
    {
        EnemyData data = new EnemyData();
        data.health = health;
        data.position = transform.position;
        data.type = gameObject.name;
        string save = JsonUtility.ToJson(data);
        File.WriteAllText(dataPath, save);

    }
    public void InitializeHealth(float start)
    {
        health = start;
    }
    public bool CanSeeTarget(Transform eyeballs)
    {
        if (target!=null)
        {
            RaycastHit hit;
            if(Physics.Raycast(eyeballs.position,target.transform.position-eyeballs.position,out hit))
            {
                
                if(hit.transform.gameObject==target)
                {
                    
                    return true;
                }
            }
        }
        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemies;
    public float spawnRate = 5f;
    private float spawnDelay = 0;
    private int difficulty = 3;
    public static int enemyNumber;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyNumber = 0;
        LoadEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        if(Overlord.IsNight())
        {
            spawnDelay += Time.deltaTime;
            if(spawnDelay > spawnRate)
            {
                spawnDelay = 0;
                Spawn();
            }
        }
    }
    void Spawn()
    {
        float xPos = Random.Range(0, Spawner.instance.chunksX * 5);
        float zPos = Random.Range(0,Spawner.instance.chunksZ * 5);
        int enemyToSpawn = Random.Range(0,difficulty);
        float yPos = (int)ChunkBlocks.GetPerlinValue(xPos, zPos)-Spawner.instance.chunksY*5+2;
        Vector3 spawnPosition = new Vector3(xPos, yPos, zPos);
        if(!Physics.CheckBox(spawnPosition,new Vector3(0.4f,0.9f,0.4f)))
        {
            Instantiate(enemies[enemyToSpawn], spawnPosition, Quaternion.identity).name = enemies[enemyToSpawn].gameObject.name;

        }
    }
    void LoadEnemies()
    {
        //DO this 
        var enemyDataFiles = Directory.EnumerateFiles(Application.persistentDataPath+"/enemies");
        foreach(string path in enemyDataFiles)
        {
            if(Overlord.OverwriteMode)
            {
                File.Delete(path);
            }
            else
            {
                string readData = File.ReadAllText(path);
                EnemyData enemy = JsonUtility.FromJson<EnemyData>(readData);
                if (enemy.health > 0)
                {
                    GameObject toSpawn = GetEnemy(enemy.type);
                    GameObject spawnedEnemy = Instantiate(toSpawn, enemy.position, Quaternion.identity);
                    spawnedEnemy.GetComponent<Enemy>().InitializeHealth(enemy.health);
                    spawnedEnemy.name = enemy.type;
                }
            }
            
        }
    }
    GameObject GetEnemy(string name)
    {
        foreach(GameObject enemy in enemies)
        {
            if(enemy.name == name)
            {
                return enemy;
            }
        }
        return null;
    }
}

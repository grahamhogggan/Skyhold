using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EnemySpawner : MonoBehaviour
{
    public EnemySet[] sets;
    public float spawnRate = 5f;
    private float spawnDelay = 0;
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
        int enemyToSpawn = Random.Range(0,Mathf.Min(Overlord.difficulty+1,sets[(int)Overlord.type].enemies.Length));
        float yPos = (int)ChunkBlocks.GetPerlinValue(xPos, zPos)-Spawner.instance.chunksY*5+2;
        Vector3 spawnPosition = new Vector3(xPos, yPos, zPos);
        if(!Physics.CheckBox(spawnPosition,new Vector3(0.4f,0.9f,0.4f)))
        {
            Instantiate(sets[(int)Overlord.type].enemies[enemyToSpawn], spawnPosition, Quaternion.identity).name = sets[(int)Overlord.type].enemies[enemyToSpawn].gameObject.name;

        }
    }
    void LoadEnemies()
    {
        //DO this 
        var enemyDataFiles = Directory.EnumerateFiles(Overlord.saveDirectory+"/enemies");
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
        foreach(GameObject enemy in sets[(int)Overlord.type].enemies)
        {
            if(enemy.name == name)
            {
                return enemy;
            }
        }
        return null;
    }
}
[System.Serializable]
public struct EnemySet
{
    public GameObject[] enemies;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public int freq = 10;
    private GameObject[] bosses;
    public EnemySpawner spawner;

    private bool notSpawnedThisNight = false;

    // Start is called before the first frame update
    void Start()
    {
        notSpawnedThisNight = false;
        bosses = spawner.bosses.enemies;
        if(Overlord.type == Overlord.WorldType.Finale&&Overlord.OverwriteMode)
        {
            GameObject boss = Instantiate(bosses[(int)Overlord.type],new Vector3(25,0,25),Quaternion.identity);
            boss.name = bosses[(int)Overlord.type].name;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Overlord.IsNight()&&Overlord.type != Overlord.WorldType.Finale)
        {
            if(Overlord.nightCount%freq == 0)
            {
                if(notSpawnedThisNight)
                {
                    GameObject boss = Instantiate(bosses[(int)Overlord.type],new Vector3(25,0,25),Quaternion.identity);
                    boss.name = bosses[(int)Overlord.type].name;
                    notSpawnedThisNight = false;
                }
            }
            else
            {
                notSpawnedThisNight = true;
            }
        }
    }
}

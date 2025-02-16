using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] thingies;
    [SerializeField] private float rate;
    private float delay;
    public float worldScale;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        delay+=Time.deltaTime;
        if(Overlord.IsNight())
        {
            if(delay>rate)
            {
                delay = 0;
                Vector3 spawnPos =new Vector3(Random.Range(0,worldScale),50,Random.Range(0,worldScale));
                Vector3 hitPos = new Vector3(Random.Range(-2,worldScale+2),-50,Random.Range(-2,worldScale+2));
                Instantiate(thingies[0],spawnPos,Quaternion.LookRotation((hitPos-spawnPos).normalized));
            }
        }
    }
}

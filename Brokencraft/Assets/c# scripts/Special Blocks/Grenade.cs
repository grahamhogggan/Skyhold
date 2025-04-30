using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GameObject shrapnel;
    public GameObject sound;
    // Start is called before the first frame update
    void Start()
    {
        Block b = transform.parent.GetComponent<Block>();
        TerroristManager.Bomb(b,5);
        for(int i = 0; i<20; i++)
        {
            Instantiate(shrapnel,transform.position, Quaternion.Euler(Random.Range(0,360),Random.Range(0,360),Random.Range(0,360)));
        }
        Instantiate(sound,transform.position,transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

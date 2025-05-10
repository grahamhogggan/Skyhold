using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrapnel : MonoBehaviour
{
    public float damage;
    public float aliveTime = 2;
    public float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().velocity = transform.forward*speed;
        aliveTime-=Time.deltaTime;
        if(aliveTime<0) Destroy(gameObject);
    }
    void OnTriggerEnter(Collider coll)
    {
        Enemy en = coll.transform.root.gameObject.GetComponentInChildren<Enemy>();
        if(en!=null)
        {   if(en.boss)
                en.Damage(damage/3);
            else
                en.Damage(damage);
        }
    }
}

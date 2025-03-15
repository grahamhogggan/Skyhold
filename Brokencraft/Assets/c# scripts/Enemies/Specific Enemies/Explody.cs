using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explody : MonoBehaviour
{
    private bool deleted;
    // Start is called before the first frame update
    void Start()
    {
        deleted = false;
    }
    void Update()
    {
    }

    // Update is called once per frame

    void OnTriggerEnter(Collider coll)
    {
        Player p =  coll.gameObject.GetComponent<Player>();
        if(p!=null&&!deleted)
        {
            p.Damage(10);
            GetComponent<Enemy>().Damage(20);
            deleted = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDamager : MonoBehaviour
{
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider coll)
    {
        Player hit = coll.gameObject.GetComponentInChildren<Player>();
        if(hit!=null)
        {
            hit.Damage(damage);
        }
    }
}

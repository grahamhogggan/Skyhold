using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBreath : MonoBehaviour
{
    public int damage;
    void OnParticleCollision(GameObject other)
    {
        Player p = other.GetComponent<Player>();
        if(p!=null)
        
        {
            p.Damage(damage);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage = 1;
    public float knockback = 0;
    public float hitDelay = 0.5f;
    
    public Vector3 GetStats()
    {
        return new Vector3(damage, knockback, hitDelay);
    }
}

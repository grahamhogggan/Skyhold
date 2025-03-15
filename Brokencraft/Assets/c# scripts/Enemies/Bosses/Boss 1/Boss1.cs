using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class Boss1 : MonoBehaviour
{
    // Start is called before the first frame update

    private Boss1Turret[] turrets;
    
    void Start()
    {
        turrets = GetComponentsInChildren<Boss1Turret>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<Enemy>().CanSeeTarget(transform))
        {
            foreach(Boss1Turret turret in turrets)
            {
                turret.Shoot(Enemy.target.transform);
            }
        }
    }
}
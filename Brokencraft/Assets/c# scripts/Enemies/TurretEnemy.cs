using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class TurretEnemy : MonoBehaviour
{
    // Start is called before the first frame update

    private Turret[] turrets;
    
    void Start()
    {
        turrets = GetComponentsInChildren<Turret>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<Enemy>().CanSeeTarget(transform))
        {
            foreach(Turret turret in turrets)
            {
                turret.Shoot(Enemy.target.transform);
            }
        }
    }
}

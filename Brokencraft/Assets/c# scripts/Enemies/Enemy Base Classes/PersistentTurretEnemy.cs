using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentTurretEnemy : MonoBehaviour
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
            foreach(Turret turret in turrets)
            {
                turret.Shoot(Enemy.target.transform);
            }
    }
}

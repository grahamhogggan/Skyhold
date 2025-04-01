using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactaboss : MonoBehaviour
{
    // Start is called before the first frame update

    private SpineLauncher[] turrets;
    public bool alwaysShoot = false;
    
    void Start()
    {
        turrets = GetComponentsInChildren<SpineLauncher>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<Enemy>().CanSeeTarget(transform)||alwaysShoot)
        {
            foreach(SpineLauncher turret in turrets)
            {
                turret.Shoot(Enemy.target.transform);
            }
        }
    }
}

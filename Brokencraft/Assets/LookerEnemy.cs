using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookerEnemy : MonoBehaviour
{
    void Update()
    {
        GameObject target = Enemy.target;
        Vector3 playerLocator = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        transform.LookAt(playerLocator);
    }

}

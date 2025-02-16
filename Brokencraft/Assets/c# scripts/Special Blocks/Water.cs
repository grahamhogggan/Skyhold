using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private float density;
    private FirstPersonMovement person;
    // Start is called before the first frame update

    void OnTriggerStay(Collider coll)
    {
        person = coll.gameObject.GetComponent<FirstPersonMovement>();
        if (person != null)
        {
            person.inWater = true ;
        }
        Rigidbody rb = coll.gameObject.GetComponent<Rigidbody>();
        if(rb!= null)
        {
            if(rb.velocity.y < density)
            {
                rb.AddForce(new Vector3(0, density*15, 0));
            }
        }
        ChasingEnemy enemy = coll.gameObject.GetComponent<ChasingEnemy>();
        if(enemy != null)
        {
            enemy.inWater = true;
        }
    }
    void OnTriggerExit(Collider coll)
    {
        person = coll.gameObject.GetComponent<FirstPersonMovement>();
        if (person != null)
        {
            person.inWater = false;
        }
        ChasingEnemy enemy = coll.gameObject.GetComponent<ChasingEnemy>();
        if (enemy != null)
        {
            enemy.inWater = false;
        }
    }
    void OnDisable()
    {
        if (person != null)
        {
            person.inWater = false;
        }
    }
}

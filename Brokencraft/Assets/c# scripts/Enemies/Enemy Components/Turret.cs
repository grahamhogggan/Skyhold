using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected float reload;
    [SerializeField] private float offset = 0;
    protected float delay = 0;
    void Start()
    {
        delay = offset;
    }

    // Update is called once per frame
    void Update()
    {
        delay+=Time.deltaTime;
    }
    public bool Shoot(Transform target)
    {   
        transform.LookAt(target);
        if(delay>reload)
        {
            Instantiate(bullet,transform.position,transform.rotation);
            delay = 0;
            return true;
        }
        return false;
    }
}

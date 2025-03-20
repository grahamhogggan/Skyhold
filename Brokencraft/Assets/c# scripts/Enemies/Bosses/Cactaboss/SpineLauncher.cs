using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineLauncher:MonoBehaviour
{
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
        Debug.Log("subclass method called");
        if(delay>reload)
        {
            Instantiate(bullet,transform.position,Quaternion.Euler(Vector3.up)).GetComponent<Spine>().target = target;
            delay = 0;
            return true;
        }
        return false;
    }
}

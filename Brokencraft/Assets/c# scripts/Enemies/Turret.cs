using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject bullet;
    [SerializeField] private float reload;
    [SerializeField] private float offset = 0;
    private float delay = 0;
    void Start()
    {
        delay = offset;
    }

    // Update is called once per frame
    void Update()
    {
        delay+=Time.deltaTime;
    }
    public void Shoot(Transform target)
    {   
        transform.LookAt(target);
        if(delay>reload)
        {
            Instantiate(bullet,transform.position,transform.rotation);
            delay = 0;
        }
    }
}

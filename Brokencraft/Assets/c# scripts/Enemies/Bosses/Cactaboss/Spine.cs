using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spine : MonoBehaviour
{
    public Vector3 initVelocity;
    public Transform target;
    private Rigidbody rb;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = initVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(rb.velocity);
        if(target == null)
        {
            Debug.Log("error");
            return;
        }
        Vector3 addForce = (target.position-transform.position)*speed;
        addForce = new Vector3(addForce.x,0,addForce.z);
        float magnitude = Mathf.Clamp(addForce.magnitude,0,speed);
        addForce = addForce.normalized*magnitude;
        rb.velocity = new Vector3(addForce.x,rb.velocity.y,addForce.z);
    }
}

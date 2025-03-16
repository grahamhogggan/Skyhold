using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Enemy
{
// Start is called before the first frame update
    private Rigidbody body;
    [SerializeField] private float floatDist;
    [SerializeField] private float speed = 5;
    [SerializeField] private GameObject head;
    void Start()
    {
        base.Start();
        body = GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        float targetHeight = transform.position.y - GetHeight()+floatDist;
        Vector3 pos = transform.position;
        if(GetHeight()>0)
        {
            pos = transform.position+(targetHeight - transform.position.y)*transform.up*Time.deltaTime;
            body.MovePosition(pos);
        }
        if(CanSeeTarget(transform))
        {
                Vector3 mov = target.transform.position-transform.position;
                mov = new Vector3(mov.x,0,mov.z);
                if(mov.magnitude>speed)
                {
                    mov.Normalize();
                    mov*=speed;
                }
                body.MovePosition(pos+mov*Time.deltaTime);

                Quaternion look = Quaternion.LookRotation(mov);
                body.MoveRotation(Quaternion.Slerp(transform.rotation,look,0.1f));

                head.transform.LookAt(target.transform.position);
        }
        else
        {
            head.transform.localRotation = Quaternion.identity;
        }

    }
    float GetHeight()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position,transform.up*-1,out hit,Mathf.Infinity,Physics.DefaultRaycastLayers,QueryTriggerInteraction.Collide))
        {
            float h = hit.distance;
            return h;
        }
        return -1;
    }
}
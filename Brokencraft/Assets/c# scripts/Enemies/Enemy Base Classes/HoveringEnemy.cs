using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoveringEnemy : Enemy
{
    // Start is called before the first frame update
    private CharacterController body;
    [SerializeField] private float floatDist;
    [SerializeField] private float speed = 5;
    public bool RotateToFacePlayer = false;
    public GameObject mesh;
    
    void Start()
    {
        base.Start();
        body = GetComponent<CharacterController>();   
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        float targetHeight = transform.position.y - GetHeight()+floatDist;
        if(GetHeight()>0)
        {
            body.Move((targetHeight - transform.position.y)*transform.up*Time.deltaTime);
        }
        if(CanSeeTarget(transform))
        {
                Vector3 mov = target.transform.position-transform.position;
                if(mov.magnitude>speed)
                {
                    mov.Normalize();
                }
                body.Move(mov*Time.deltaTime);
                if(RotateToFacePlayer)
                {
                    mesh.transform.LookAt(target.transform.position);
                }
        }

    }
    float GetHeight()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position,transform.up*-1,out hit,Mathf.Infinity,~(1 << gameObject.layer),QueryTriggerInteraction.Collide))
        {
            float h = hit.distance;
            return h;
        }
        return -1;
    }
}

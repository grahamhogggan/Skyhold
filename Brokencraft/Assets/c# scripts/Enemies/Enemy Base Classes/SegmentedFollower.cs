using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SegmentedFollower : MonoBehaviour
{
    [SerializeField] private SegmentedFollower follow;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float followDistance = 1.5f;

    public Vector3 Velocity;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(follow!=null)
        {
            Vector3 targetPosition = follow.transform.position+-1*followDistance*follow.transform.forward;
            rb.rotation = (Quaternion.LookRotation(targetPosition-transform.position));
            rb.velocity = transform.forward*speed*(Vector3.Distance(transform.position,targetPosition));

        }
        Velocity = rb.velocity;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starfighter : Enemy
{
    // Start is called before the first frame update
    private Rigidbody body;
    [SerializeField] private float floatHeight;
    [SerializeField] private float minDistance;
    [SerializeField] private float speed = 5;
    [SerializeField] private GameObject head;
    void Start()
    {
        base.Start();
        body = GetComponent<Rigidbody>();
        head.transform.localRotation = Quaternion.Euler(0, -90, 0);
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        float targetHeight = floatHeight;
        Vector3 pos = transform.position;
            pos = transform.position + (targetHeight - transform.position.y) * transform.up * Time.deltaTime;
            body.MovePosition(pos);
        Vector2 latOffset = new Vector2(target.transform.position.x - transform.position.x, target.transform.position.z - transform.position.z);
        if (CanSeeTarget(transform))
        {
            Vector3 mov = target.transform.position - transform.position;
            mov = new Vector3(mov.x, 0, mov.z);
            if (mov.magnitude > speed)
            {
                mov.Normalize();
                mov *= speed;
            }

            if (latOffset.magnitude > minDistance)
            {
                body.MovePosition(pos + mov * Time.deltaTime);

            }

            LookAtPlayer(mov);
        }
        else if (latOffset.magnitude >= minDistance)
        {
            LookAtPlayer(target.transform.position - transform.position);
        }


    }
    void LookAtPlayer(Vector3 dir)
    {
        Quaternion look = Quaternion.LookRotation(dir);
        body.MoveRotation(Quaternion.Slerp(transform.rotation, look, 0.1f));

        head.transform.LookAt(target.transform.position);
    }
    float GetHeight()
    {
        return transform.position.y;
    }
}
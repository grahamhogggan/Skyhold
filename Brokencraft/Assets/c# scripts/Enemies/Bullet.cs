using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody rb;
    private bool deleted = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        deleted = false;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.forward*speed;
    }
    void OnTriggerEnter(Collider coll)
    {
        if(deleted) return;
        Player p = coll.GetComponentInChildren<Player>();
        if(p !=null)
        {
            p.Damage(1);
            Destroy(gameObject);
            deleted = true;
        }
        if(coll.gameObject.GetComponent<BlockDetails>())
        {
            Destroy(gameObject);
            deleted = true;
        }
    }
}

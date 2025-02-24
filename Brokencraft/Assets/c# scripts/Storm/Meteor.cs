using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool deleted = false;
    // Start is called before the first frame update
    void Start()
    {
        deleted = false;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().velocity = transform.forward*speed;
    }
    void OnCollisionEnter(Collision coll)
    {
        GameObject hitObj = coll.gameObject;
        Collide(hitObj);
        
    }
    void OnTriggerEnter(Collider coll)
    {
        GameObject hitObj = coll.gameObject;
        Collide(hitObj);
    }


    void Collide(GameObject hitObj)
    {
        if(deleted) return;
        Destroy(gameObject);
        Block hitBlock = hitObj.GetComponentInParent<Block>();
        if(hitBlock!=null)
        {
            TerroristManager.Bomb(hitBlock,Mathf.Min(3+(int)Overlord.difficulty/3,6));


        }
        ScoreManager.AddScore(5);
        deleted = true;
    }

}

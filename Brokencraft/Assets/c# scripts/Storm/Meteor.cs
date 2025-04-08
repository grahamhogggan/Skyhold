using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject soundEffectOnImpact;
    private bool deleted = false;
    // Start is called before the first frame update
    void Start()
    {
        deleted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (speed == 0) return;
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
            Instantiate(soundEffectOnImpact,transform.position,Quaternion.identity);

        }
        ScoreManager.AddScore(5);
        deleted = true;
    }

}

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
    void Bomb(Block block, int depth)
    {
        if(block==null||!block.gameObject.activeSelf) return;
        if(depth == 0)
        {
            block.Mine();
        }
        else
        {

        Bomb(block.GetAdjacent(new Vector3(0,0,1)),depth-1);
        Bomb(block.GetAdjacent(new Vector3(0,0,-1)),depth-1);
        Bomb(block.GetAdjacent(new Vector3(0,1,0)),depth-1);
        Bomb(block.GetAdjacent(new Vector3(0,-1,0)),depth-1);
        Bomb(block.GetAdjacent(new Vector3(1,0,0)),depth-1);
        Bomb(block.GetAdjacent(new Vector3(-1,0,0)),depth-1);
        
            block.Mine();
        }
    }
    void Collide(GameObject hitObj)
    {
        if(deleted) return;
        Destroy(gameObject);
        Block hitBlock = hitObj.GetComponentInParent<Block>();
        if(hitBlock!=null)
        {
            Bomb(hitBlock,6);


        }
        ScoreManager.AddScore(5);
        deleted = true;
    }
}

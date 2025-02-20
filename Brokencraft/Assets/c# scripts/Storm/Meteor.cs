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
    public static void Bomb(Block block, int depth)
    {
        if(block==null||!block.gameObject.activeSelf) return;  
        if(block.gameObject.GetComponentInChildren<Durable>())
        {
            depth-= block.gameObject.GetComponentInChildren<Durable>().bonusStrength;
        }
            List<Block> nextLayer = new List<Block>();
            List<Block> lastLayer = new List<Block>();
            lastLayer.Add(block);
        for(int i = 0; i<depth; i++)
        {
            foreach(Block b in lastLayer)
            {
            nextLayer.AddRange(BombShell(b));

            }
            lastLayer = nextLayer;
            nextLayer =  new List<Block>();
        }


    }
    private static List<Block> BombShell(Block block)
    {
        
        if(block.gameObject.activeSelf)
        {
            block.Mine();
        }
        List<Block> adjacents = new List<Block>();
        Block b = block.GetAdjacent(new Vector3(0,0,1));
        if(b!=null)
        {
            adjacents.Add(b);
        }
        b = (block.GetAdjacent(new Vector3(0,0,-1)));
                if(b!=null&&b.gameObject.activeSelf&&!b.gameObject.GetComponent<Durable>())
        {
            adjacents.Add(b);
        }
        b = (block.GetAdjacent(new Vector3(0,1,0)));
                if(b!=null&&b.gameObject.activeSelf&&!b.gameObject.GetComponent<Durable>())
        {
            adjacents.Add(b);
        }
        b = (block.GetAdjacent(new Vector3(0,-1,0)));
                if(b!=null&&b.gameObject.activeSelf&&!b.gameObject.GetComponent<Durable>())
        {
            adjacents.Add(b);
        }
        b = (block.GetAdjacent(new Vector3(1,0,0)));
                if(b!=null&&b.gameObject.activeSelf&&!b.gameObject.GetComponent<Durable>())
        {
            adjacents.Add(b);
        }
        b = (block.GetAdjacent(new Vector3(-1,0,0)));
            if(b!=null&&b.gameObject.activeSelf&&!b.gameObject.GetComponent<Durable>())
        {
            adjacents.Add(b);
        }        if(b!=null&&b.gameObject.activeSelf&&!b.gameObject.GetComponent<Durable>())
        {
            adjacents.Add(b);
        }
        return adjacents;

        
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

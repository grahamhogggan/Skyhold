using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerroristManager : MonoBehaviour
{
    public static TerroristManager instance;
    // Start is called before the first frame update
    void Start()
    {
        if(instance!=null)
        {
            Debug.Log("you messed up");
        }
        instance=  this;

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
     IEnumerator BombRoutine(Block block,int depth)
    {
         List<Block> nextLayer = new List<Block>();
        List<Block> lastLayer = new List<Block>();
        lastLayer.Add(block);
        for(int i = 0; i<depth; i++)
        {
            yield return new WaitForSeconds(0.1f);
            foreach(Block b in lastLayer)
            {
            nextLayer.AddRange(BombShell(b));

            }
            lastLayer = nextLayer;
            nextLayer =  new List<Block>();
        }
    }
    public static void Bomb(Block block, int depth)
    {
        if(block==null||!block.gameObject.activeSelf) return;  
        if(block.gameObject.GetComponentInChildren<Durable>())
        {
            depth-= block.gameObject.GetComponentInChildren<Durable>().bonusStrength;
        }
        instance.StartCoroutine(instance.BombRoutine(block,depth));


    }
    
}

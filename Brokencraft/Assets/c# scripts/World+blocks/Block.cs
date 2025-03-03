using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Chunk parentChunk;
    public Vector3 localPos;
    [SerializeField] private GameObject substance;

    public string typeName = "dirt";

    // Start is called before the first frame update
    void Start()
    {
        parentChunk = transform.root.GetComponent<Chunk>();
        localPos = new Vector3((int)transform.localPosition.x, (int)transform.localPosition.y, (int)transform.localPosition.z);
        gameObject.name = "Block_"+localPos.ToString();

    }

    public void Mine()
    {
        
        gameObject.SetActive(false);
        parentChunk.RecordChange("mined/0", this);
        
        if(localPos.y==0)
        {
           
            parentChunk.GetNearChunk(new Vector3(0, 1, 0)).Expose();
        }
        if (localPos.y == 4)
        {
            
            parentChunk.GetNearChunk(new Vector3(0, -1, 0)).Expose();
        }
        if (localPos.x == 0)
        {
            
            parentChunk.GetNearChunk(new Vector3(-1, 0, 0)).Expose();
        }
        if (localPos.x == 4)
        {
            
            parentChunk.GetNearChunk(new Vector3(1, 0, 0)).Expose();
        }
        if (localPos.z == 0)
        {
           
            parentChunk.GetNearChunk(new Vector3(0, 0, 1)).Expose();
        }
        if (localPos.z == 4)
        {
            
            parentChunk.GetNearChunk(new Vector3(0, 0, -1)).Expose();
        }
        if(parentChunk.IsEmpty())
        {
            
            parentChunk.Hide();
        }

        Vector3[] directions = 
        {
         new Vector3(0, 0, 1),  // Forward
        new Vector3(1, 0, 0), // Right

        new Vector3(0, 0, -1),  // Backward
        new Vector3(-1, 0, 0), // Left
        new Vector3(0, 1, 0), // up


        };
        foreach(Vector3 dir in directions)
        {
            Block a = GetAdjacent(dir);
            if(a!=null&&a.gameObject.GetComponentInChildren<Water>())
            {
                a.GetComponentInChildren<Water>().isExposed = true;
            }
        }
    }
    public void Place(string type,int rotation)
    {
        //Debug.Log("Placed");
        gameObject.SetActive(true);
        transform.root.GetComponent<Chunk>().RecordChange("place: "+type+"/"+rotation, this);
        SetBlock(type);
        //Debug.Log("block placed in chunk, exposing");
        transform.root.GetComponent<Chunk>().Expose();

        transform.rotation = Quaternion.Euler(new Vector3(0,rotation,0));
        if(type.Length>=5&&type.Substring(0,5)=="water")
        {
        Vector3[] directions = 
        {
         new Vector3(0, 0, 1),  // Forward
        new Vector3(1, 0, 0), // Right

        new Vector3(0, 0, -1),  // Backward
        new Vector3(-1, 0, 0), // Left
        new Vector3(0, 1, 0), // up


        };
        foreach(Vector3 dir in directions)
        {
            Block a = GetAdjacent(dir);
            if(a!=null&&a.gameObject.GetComponentInChildren<Water>())
            {
                a.GetComponentInChildren<Water>().isExposed = true;
            }
        }
        }

    }
    public void Place(string type)
    {
        Place(type,0);
    }

    public Block GetAdjacent(Vector3 dir)
    {
        //Debug.Log("adjacentinging");
        int x = (int)localPos.x+(int)dir.x;
        int y = (int)localPos.y+(int)dir.y;
        int z = (int)localPos.z+(int)dir.z;
        Chunk currentChunk = parentChunk;
        if(x<0)
        {
            currentChunk = currentChunk.GetNearOtherChunk(new Vector3(-1, 0, 0));
            x = 4;
        }
        if (z < 0)
        {
            currentChunk = currentChunk.GetNearOtherChunk(new Vector3(0, 0, -1));
            z = 4;
        }
        if (y < 0)
        {
            currentChunk = currentChunk.GetNearOtherChunk(new Vector3(0, 1, 0));
            y = 4;
        }
        if (x > 4)
        {
            currentChunk = currentChunk.GetNearOtherChunk(new Vector3(1, 0, 0));
            x = 0;
        }
        if (z > 4)
        {
            currentChunk = currentChunk.GetNearOtherChunk(new Vector3(0, 0, 1));
            z = 0;
        }
        if (y > 4)
        {
            currentChunk = currentChunk.GetNearOtherChunk(new Vector3(0, -1, 0));
            y = 0;
        }

        if(currentChunk != null)
        {
            //Debug.Log("Successful return");
            return currentChunk.GetBlock(x, y, z);

        }
        //Debug.Log("null return");
        return null;

    }
    public void SetBlock(string materialName)
    {
        typeName = materialName;
        GameObject block = Miner.GetBlock(materialName);
        if(block != null)
        {
            Destroy(substance);
            substance = Instantiate(block, transform.position, transform.rotation);
            substance.transform.SetParent(this.transform);
            //Debug.Log("successful placement");
        }
        else
        {
            //Debug.Log("no such block");
        }

        
    }
    public bool IsCellClear()
    {
        return !Physics.CheckBox(transform.position, new Vector3(0.4f, 0.4f, 0.4f));
    }
    public Chunk getChunk()
    {
        return parentChunk;
    }
    public Vector3 GetLocalPos()
    {
        return localPos;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private float density;
    private FirstPersonMovement person;
    // Start is called before the first frame update
    public bool isExposed = true;
    static List<Water> blocks = new List<Water>();
    void Start()
    {
        blocks.Add(this);
                StartCoroutine(WaterSpreadRoutine()); // Begin spreading

    }
    IEnumerator WaterSpreadRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.25f, 1f)); // Random delay for smooth spreading

            WaterTick();
        }
    }
    void OnDestroy()
    {
        blocks.Remove(this);
    }
    void OnTriggerStay(Collider coll)
    {
        person = coll.gameObject.GetComponent<FirstPersonMovement>();
        if (person != null)
        {
            person.inWater = true;
        }
        Rigidbody rb = coll.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            if (rb.velocity.y < density)
            {
                rb.AddForce(new Vector3(0, density * 15, 0));
            }
        }
        ChasingEnemy enemy = coll.gameObject.GetComponent<ChasingEnemy>();
        if (enemy != null)
        {
            enemy.inWater = true;
        }
    }
    void OnTriggerExit(Collider coll)
    {
        person = coll.gameObject.GetComponent<FirstPersonMovement>();
        if (person != null)
        {
            person.inWater = false;
        }
        ChasingEnemy enemy = coll.gameObject.GetComponent<ChasingEnemy>();
        if (enemy != null)
        {
            enemy.inWater = false;
        }
    }
    void OnDisable()
    {
        if (person != null)
        {
            person.inWater = false;
        }
    }
    void WaterTick()
{
    if(isExposed == false) return;
    Block block = GetComponentInParent<Block>();
    if (block == null) return;

    // List of directions to check in order: Down, Right, Left, Forward, Backward
    Vector3[] directions = 
    {
        new Vector3(0, 0, 1),  // Forward
        new Vector3(1, 0, 0), // Right

        new Vector3(0, 0, -1),  // Backward
        new Vector3(-1, 0, 0), // Left
        new Vector3(0, -1, 0), // Down (priority)


    };

    for(int i = directions.Length-1;i>=0; i--)
    {
        Vector3 dir = directions[i];
        Block adjacent = block.GetAdjacent(dir);
        if (adjacent != null && !adjacent.gameObject.activeSelf)
        {
            if(dir.y == -1)
            {

                adjacent.Place("water");
            }
            else if(block.typeName != "small water")
            {
                Block below = block.GetAdjacent(new Vector3(0,-1,0));
                Block belowTarget = adjacent.GetAdjacent(new Vector3(0,-1,0));
                if(below!=null&&(below.typeName!="water"&&below.typeName!="small water"&&below.gameObject.activeSelf))
                {
                adjacent.Place("small water",90*i);
                Debug.Log("case 1");
                }
                else if(belowTarget!=null&&belowTarget.gameObject.activeSelf&&belowTarget.typeName!="small water")
                {
                    Debug.Log("case 2");
                                    adjacent.Place("small water",90*i);

                }
            }            
            return; // Stop after placing water once
        }
        else if(adjacent !=null &&adjacent.typeName == "small water"&&block.typeName != "small water"&&!(adjacent.transform.rotation.eulerAngles.y==90*i))
        {
                Debug.Log(adjacent.transform.rotation.eulerAngles.y+"/"+90*i);
                            adjacent.Place("water");
                        return;
        }
        else if(dir.y == -1 && adjacent!=null&&block.typeName == "small water"&&adjacent.typeName == "small water")
        {
                            adjacent.Place("water");
        return;
        }
    }
            isExposed = false;

}
}


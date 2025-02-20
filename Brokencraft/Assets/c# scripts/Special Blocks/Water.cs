using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private float density;
    private FirstPersonMovement person;
    // Start is called before the first frame update
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
            yield return new WaitForSeconds(Random.Range(1f, 5f)); // Random delay for smooth spreading

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
    Block block = GetComponentInParent<Block>();
    if (block == null) return;

    // List of directions to check in order: Down, Right, Left, Forward, Backward
    Vector3[] directions = 
    {
        new Vector3(0, -1, 0), // Down (priority)
        new Vector3(1, 0, 0),  // Right
        new Vector3(-1, 0, 0), // Left
        new Vector3(0, 0, 1),  // Forward
        new Vector3(0, 0, -1)  // Backward
    };

    foreach (Vector3 dir in directions)
    {
        Block adjacent = block.GetAdjacent(dir);
        if (adjacent != null && !adjacent.gameObject.activeSelf)
        {
            adjacent.Place("water");
            block.Mine();
            return; // Stop after placing water once
        }
    }
}
}

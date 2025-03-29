using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    public string item;
    public int ID;
    public GameObject collectSound;
    // Start is called before the first frame update
    void Start()
    {
        SetViewer();
    }

    // Update is called once per frame
    void Update()
    {
        Overlord.instance.dropData.positions[ID] = transform.position;
    }
    void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.GetComponent<Player>())
        {
            PlayerInventory.StaticAddItem(item);
            Overlord.DeleteDrop(ID);
            Instantiate(collectSound);
            Destroy(gameObject);
        }
    }
    void SetViewer()
    {
        GameObject block = Miner.GetBlock(item);
        GameObject display = block.GetComponentInChildren<MeshUnifier>().meshRoot;
        GameObject viewer = Instantiate(display, transform.position, transform.rotation);
        viewer.transform.SetParent(transform);
        viewer.transform.localScale /= 2;
    }
}

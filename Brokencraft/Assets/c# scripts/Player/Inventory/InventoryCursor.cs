using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCursor : MonoBehaviour
{
    // Start is called before the first frame update
    public Image image;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int i = PlayerInventory.clickedItemIndex;
        if(i==-1)
        {
            image.enabled = (false);
        }
        else
        {
            image.sprite = PlayerInventory.instance.GetBlockPreview(PlayerInventory.instance.GetItem(i));
            image.enabled = true;
        }
        transform.position = Input.mousePosition+new Vector3(25,25,25);
    }
}

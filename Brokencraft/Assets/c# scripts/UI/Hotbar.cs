using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
    [SerializeField] private Image centerCell;
    [SerializeField] private Image plus1;
    [SerializeField] private Image plus2;
    [SerializeField] private Image minus1;
    [SerializeField] private Image minus2;
    [SerializeField] private GameObject player;
    private PlayerInventory inventory;
    private Miner miner;
    // Start is called before the first frame update
    void Start()
    {
        miner = player.GetComponentInChildren<Miner>();
        inventory = player.GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inventory.GetItemCount() >0)
        {
            centerCell.enabled = true;
            int index = miner.GetEquippedIndex();
            string centralItem = inventory.GetItem(index);
            //Debug.Log(centralItem);
            Sprite centralSprite = inventory.GetBlockPreview(centralItem);
            centerCell.sprite = centralSprite;
        }
        else
        {
            centerCell.enabled = false;
        }
        
        if(inventory.GetItemCount() >0)
        {
            // previous
            int indexm1 = miner.GetEquippedIndex() - 1;
            if (indexm1 >= 0)
            {
                minus1.enabled = true;

                string itemm1 = inventory.GetItem(indexm1);
                Sprite spritem1 = inventory.GetBlockPreview(itemm1);
                minus1.sprite = spritem1;

            }
            else
            {
                minus1.enabled = false;
            }


            int indexm2 = miner.GetEquippedIndex() - 2;
            if (indexm2 >= 0)
            {
                minus2.enabled = true;
                string itemm2 = inventory.GetItem(indexm2);
                Sprite spritem2 = inventory.GetBlockPreview(itemm2);
                minus2.sprite = spritem2;
            }
            else
            {
                minus2.enabled = false;

            }
            //post


            int indexp1 = miner.GetEquippedIndex() + 1;
            if (indexp1 < inventory.GetItemCount())
            {
                plus1.enabled = true;
                string itemp1 = inventory.GetItem(indexp1);
                Sprite spritep1 = inventory.GetBlockPreview(itemp1);
                plus1.sprite = spritep1;
            }
            else
            {
                plus1.enabled = false;

            }

            int indexp2 = miner.GetEquippedIndex() + 2;
            if (indexp2 < inventory.GetItemCount())
            {
                plus2.enabled = true;

                string itemp2 = inventory.GetItem(indexp2);
                Sprite spritep2 = inventory.GetBlockPreview(itemp2);
                plus2.sprite = spritep2;
            }
            else
            {
                plus2.enabled = false;
            }



        }
        else
        {
            minus1.enabled = false;
            minus2.enabled = false;
            plus1.enabled = false;
            plus2.enabled = false;
            centerCell.enabled = false;
        }

    }
}

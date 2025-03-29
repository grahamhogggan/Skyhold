using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text qText;
    [SerializeField] private Image Sprite;
    public Sprite sprite;
    public string item;
    public int quantity;    
    public AudioSource clickSound;
    public void Refresh()
    {
        if(quantity == 0)
        {
            //gameObject.SetActive(false);
            nameText.text = string.Empty;
            qText.text = string.Empty;
            Sprite.enabled = false;
        }
        else
        {
            gameObject.SetActive(true);
            nameText.text = item;
            qText.text = "x" + quantity;
            Sprite.sprite = this.sprite;
            Sprite.enabled = true;
        }

    }
    public void AddToCrafting()
    {
        if(CraftingManager.AddItemToCrafting(item))
        {
            PlayerInventory.StaticRemoveItem(item);
            PlayerInventory.StaticRefresh();
        }

    }
    public void OnClick()
    {
        clickSound.Play();
        if(PlayerInventory.clickedItemIndex == -1)
        {
            PlayerInventory.clickedItemIndex = PlayerInventory.indexOf(item);
        }
        else
        {
            int index = PlayerInventory.indexOf(item);
            PlayerInventory.instance.SwapClicked(index);
            PlayerInventory.clickedItemIndex = -1;
        }
    }

}

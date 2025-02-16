using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerInventory : MonoBehaviour
{
    Inventory inventory;
    [SerializeField] private GameObject inventoryPanel;

    public List<Sprite> blockPreviews;
    private string inventorySavePath;

    public static PlayerInventory instance;

    public int offset;

    public static int clickedItemIndex;
    // Start is called before the first frame update
    void Start()
    {
        clickedItemIndex = -1;
        instance = this;
        inventorySavePath = Application.persistentDataPath + "/inventoryData.inventoryData";
        if (!File.Exists(inventorySavePath)||Overlord.OverwriteMode)
        {
            inventory = new Inventory();
        }
        else
        {
            LoadInventory();
        }
        SaveInventory();
        offset = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("tab") && Spawner.finishedLoading)
        {
            if (inventoryPanel.activeSelf)
            {
                inventoryPanel.SetActive(false);
                ClearCrafting();
                Overlord.Resume();
            }
            else
            {
                inventoryPanel.SetActive(true);
                RefreshInventory();
                Overlord.Pause();
            }
        }
        if(Input.GetMouseButtonDown(1)&&clickedItemIndex!=-1)
        {
            ToCrafting();
        }

        if(Input.GetKeyDown("a")&&inventoryPanel.activeSelf)
        {
            ChangeOffset(-1);
        }
        if(Input.GetKeyDown("d")&&inventoryPanel.activeSelf)
        {
            ChangeOffset(1);
        }
    }
    void SaveInventory()
    {
        string inventoryData = JsonUtility.ToJson(inventory);
        File.WriteAllText(inventorySavePath, inventoryData);

    }
    void LoadInventory()
    {
        inventory = JsonUtility.FromJson<Inventory>(File.ReadAllText(inventorySavePath));
    }
    public void RefreshInventory()
    {
        InventorySlot[] slots = inventoryPanel.GetComponentsInChildren<InventorySlot > ();
        foreach(InventorySlot slot in slots)
        {
            slot.item = null;
            slot.quantity = 0;
        }
        if(inventory.items.Count>0)
        {
            for(int i = 0; i<slots.Length; i++)
            {
                InventorySlot slot = slots[i];
                int index = slots.Length*offset+i;
                string item = inventory.GetItem(index);
                if(item!=null)
                {
                    slot.item = item;
                    slot.quantity = inventory.Quantity(item);
                }
            }
        }
        
        foreach (InventorySlot slot in slots)
        {
            slot.sprite = GetBlockPreview(slot.item);
            slot.Refresh();
        }

        GameObject hotkeysDisp = inventoryPanel.GetComponent<InventoryDisplay>().hotkeysDisp;
        if(offset == 0)
        {
            hotkeysDisp.SetActive(true);
        }
        else
        {
            hotkeysDisp.SetActive(false);
        }
        SaveInventory();
    }
    public Sprite GetBlockPreview(string name)
    {
        foreach(Sprite p in blockPreviews)
        {
            if(p.name==name)
            {
                return p;

            }
        }
        return null;
    }
    public void AddItem(string name)
    {
        this.inventory.AddItem(name);
        SaveInventory();
    }
    public void RemoveItem(string name)
    {
        this.inventory.RemoveItem(name);
        SaveInventory();
    }
    public string GetItem(int index)
    {
        //Debug.Log("fetched item "+ this.inventory.GetItem(index) + " at index " + index);
        return this.inventory.GetItem(index);
    }
    public int GetItemCount()
    {
        //Debug.Log("Items in inventory: " + inventory.ItemCount());

        return inventory.ItemCount();
    }
    public static void StaticRefresh()
    {
        instance.RefreshInventory();
    }
    public static void StaticAddItem(string name)
    {
        instance.inventory.AddItem(name);
        instance.SaveInventory();
    }
    public static void StaticRemoveItem(string name)
    {
        instance.inventory.RemoveItem(name);
        instance.SaveInventory();
    }
    public static void ClearCrafting()
    {
        while(CraftingManager.Cnt() > 0)
        {
            CraftingManager.RemoveLatest();
        }
    }
    void OnApplicationQuit()
    {
        ClearCrafting();
    }
    public void ChangeOffset(int howMuch)
    {
        offset+=howMuch;
        if(offset<0)
        {
            offset = 0;
        }
        if(offset*12>inventory.ItemCount())
        {
            offset--;
        }
        RefreshInventory();
    }
    public static bool ContainsItem(string i)
    {
        return instance.inventory.Quantity(i)>0;
    }
    public static int indexOf(string item)
    {
        for(int i = 0; i<instance.inventory.ItemCount(); i++)
        {
            if(instance.inventory.GetItem(i)==(item))
            {
                return i;
            }
        }
        return -1;
    }
    public void SwapClicked(int index)
    {
        inventory.SwitchItems(index,clickedItemIndex);
        RefreshInventory();
        Debug.Log("Switched items "+index+" and "+clickedItemIndex);
    }
    public void ToCrafting()
    {
        if(clickedItemIndex == -1)
        {
            return;
        }
        string currentItem = GetItem(clickedItemIndex);
        if(CraftingManager.AddItemToCrafting(inventory.GetItem(clickedItemIndex)))
        {
            RemoveItem(inventory.GetItem(clickedItemIndex));
        }
        clickedItemIndex = -1;
        RefreshInventory();
    }
    
}

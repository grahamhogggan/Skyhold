using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RecipeDisplay : MonoBehaviour
{
    public int index;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private TMP_Text buttonText;

    [SerializeField] private Image img;

    [SerializeField]private GameObject toggle;

    private string[] items;

    public void Craft()
    {
        if(PlayerCanCraft())
        {
            PlayerInventory.ClearCrafting();
            foreach(string items in items)
            {
                PlayerInventory.AddItemToCrafting(items);
            }
            CraftingManager.Craft();
        }
    }
    void Update()
    {
        string[] dat = null;
        if(RecipeDisplayManager.craftableMode)
        {
            int n = GetNthCraftableRecipe(index+RecipeDisplayManager.offset+1);
            dat = CraftingManager.GetRecipe(n,RecipeDisplayManager.category);
        }
        else
        {
            dat = CraftingManager.GetRecipe(index+RecipeDisplayManager.offset,RecipeDisplayManager.category);
        }
        
        if(dat == null)
        {
            toggle.SetActive(false);
            return;
        }
        else
        {
            toggle.SetActive(true);
        }
        nameText.text = dat[1].Split("/")[0]+" x"+dat[1].Split("/")[1];

        string cost = "";
        items= dat[0].Split(",");
        foreach(string str in items)
        {
            cost+=str;
            cost+="\n";
        }
        costText.text = cost;

        if(PlayerCanCraft())
        {
            buttonText.color = Color.white;
        }
        else
        {
            buttonText.color = Color.red;
        }
        img.sprite = PlayerInventory.instance.GetBlockPreview(dat[1].Split("/")[0]);
    }

    private int GetNthCraftableRecipe(int n)
    {
        int cnt = 0;
        int i = 0;
        while(cnt<n&&i<CraftingManager.RecipeCount(RecipeDisplayManager.category))
        {
            items = CraftingManager.GetRecipe(i,RecipeDisplayManager.category)[0].Split(",");
            if(PlayerCanCraft())
            {
                cnt++;
            }
            i++;
        }
        if(cnt!=n)
        {
            return -1;
        }
        return i-1;            
    }
    bool PlayerCanCraft()
    {
        List<string> ls = new List<string>(items);
        foreach(string s in PlayerInventory.instance.inventory.items)
        {
            if(ls.Contains(s))
            {
                ls.Remove(s);
            }
        }
        if(ls.Count == 0)
        {
            return true;
        }
        return false;

    }
}

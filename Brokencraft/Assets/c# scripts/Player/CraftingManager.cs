using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CraftingManager : MonoBehaviour
{
    private static List<string> items = new List<string>();
    private static int maxItems = 5;
    static TMP_Text display;
    [SerializeField] private TMP_Text itemsDisplayText;
    [SerializeField] private TMP_Text craftedItemText;
    private static Dictionary<string, string> craftingRecipes;
    // Start is called before the first frame update
    void Start()
    {
        display = itemsDisplayText;
        Recipize();
    }

    // Update is called once per frame
    void Update()
    {
        string total = string.Empty ;
        foreach (string item in items)
        {
            total += item + "\n";
        }
        display.text = total;
        string[] hypotheticalResult = CraftingResult();
        if(hypotheticalResult!=null)
        {
            craftedItemText.text = hypotheticalResult[0] + " x" + hypotheticalResult[1];
            if(Input.GetKeyDown("e"))
            {
                if(Input.GetKey("left shift"))
                {
                RepeatCraft();

                }
                else
                {
                    Craft();
                }
            }

        }
        else
        {
            craftedItemText.text = "Invalid";
        }
                    if(Input.GetKeyDown("q"))
            {
                RemoveLatest();
            }
    }
    public static bool AddItemToCrafting(string name)
    {
        if(items.Count<maxItems)
        {
            items.Add(name);
            return true;
        }
        return false;
    }
    public static void RemoveLatest()
    {
        if(items.Count>0)
        {
            string removed = items[items.Count - 1];
            items.RemoveAt(items.Count - 1);
            PlayerInventory.StaticAddItem(removed);
            PlayerInventory.StaticRefresh();
        }

    }
    public static void Craft()
    {
        string[] result = CraftingResult();
        if (result != null)
        {
            int quantity = int.Parse(result[1]);
            items.Clear();
            for(int i = 0; i < quantity; i++)
            {
                PlayerInventory.StaticAddItem(result[0]);

            }
            PlayerInventory.StaticRefresh();
        }
        
        
    }
    public static int RecipeCount()
    {
        return craftingRecipes.Count;
    }
    public void RepeatCraft()
    {
        string[] result = CraftingResult();
        if (result != null)
        {
            int quantity = int.Parse(result[1]);
            List<string> itemsN = new List<string>();
            for(int i = 0; i<items.Count; i++)
            {
                string item  = items[i];
                if(PlayerInventory.ContainsItem(item))
                {
                    PlayerInventory.StaticRemoveItem(item);
                    itemsN.Add(item);

                }
            }
            items = itemsN;
            for(int i = 0; i < quantity; i++)
            {
                PlayerInventory.StaticAddItem(result[0]);

            }
            PlayerInventory.StaticRefresh();
        }

    }
    private static string[] CraftingResult()
    {
        string key = string.Empty;
        List<string> init = new List<string>(items);
        items.Sort();
        foreach (string item in items)
        {
            key += item+",";
        }
        items = init;
        if(key.Length>0)
        {
        key = key.Substring(0,key.Length-1);

        }

        if (craftingRecipes.ContainsKey(key))
        {
            string[] result = craftingRecipes[key].Split('/');
            return result;
        }
        return null;
    }

    public static string[] GetRecipe(int index)
    {
        if(index>=craftingRecipes.Count||index<0)
        {
            return null;
        }
        string[] results = new string[2];
        string[] k = new string[craftingRecipes.Count];
        string[] v = new string[craftingRecipes.Count];
        craftingRecipes.Keys.CopyTo(k,0);
        craftingRecipes.Values.CopyTo(v,0);
        results[0] = k[index];
        results[1] = v[index];
        return results;
    }
    private void Recipize()
    {
        craftingRecipes = new Dictionary<string, string>();
        craftingRecipes.Add("wood", "planks/2");
        craftingRecipes.Add("palm wood","planks/1");
        craftingRecipes.Add("planks", "stick/3");
        craftingRecipes.Add("pebble,pebble,pebble,pebble,pebble","stone/1");
        craftingRecipes.Add("stone,stone", "stone slab/2");
        craftingRecipes.Add("stone slab,stone slab","ramp/1");
        craftingRecipes.Add("planks,planks,stick,stick", "pickaxe/1");
        craftingRecipes.Add("stick,stone,stone","mace/1");
        craftingRecipes.Add("metal,metal,metal","beam/1");
        craftingRecipes.Add("beam,beam,beam,beam","truss/1");
        craftingRecipes.Add("beam,metal,stick", "sword/1");
        craftingRecipes.Add("metal,stick,wires", "torch/2");
        craftingRecipes.Add("metal,wires,wires,wires","processor/1");
        craftingRecipes.Add("leaves,leaves,leaves,leaves,leaves", "dirt/1");
        
        craftingRecipes.Add("beam,metal,processor,wires,wires", "extracter/1");
        craftingRecipes.Add("beam,metal,processor,torch,wires","score pump/1");
        craftingRecipes.Add("beam,fan,processor,truss,wires","turbine/1");
        craftingRecipes.Add("beam,leaves,processor,truss,wires","biofuel plant/1");
        craftingRecipes.Add("beam,processor,qubit,truss,wires","reactor/1");
        
        
        
        
        
        
        
        
        
        

    }
    public static int Cnt()
    {return items.Count; }
}

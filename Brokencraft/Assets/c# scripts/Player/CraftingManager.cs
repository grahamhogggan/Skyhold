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
    private static Dictionary<string,Dictionary<string,string>> recipeBooks;
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
    public static int RecipeCount(string category = "all")
    {
        if(category == "all")
        return craftingRecipes.Count;
        return recipeBooks[category].Count;
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

    public static string[] GetRecipe(int index,string category = "all")
    {
        Dictionary<string,string> book;
        if(category == "all")
        {
            book = craftingRecipes;
        }
        else
        {
            book = recipeBooks[category];
        }
        if(index>=book.Count||index<0)
        {
            return null;
        }
        string[] results = new string[2];
        string[] k = new string[book.Count];
        string[] v = new string[book.Count];
        book.Keys.CopyTo(k,0);
        book.Values.CopyTo(v,0);
        results[0] = k[index];
        results[1] = v[index];
        return results;
    }
    private void Recipize()
    {
        craftingRecipes = new Dictionary<string, string>();
        recipeBooks = new Dictionary<string, Dictionary<string, string>>();
        AddRecipe("wood", "planks/2","basics");
        AddRecipe("palm wood","planks/1","basics");
        AddRecipe("planks", "stick/3","basics");
        AddRecipe("pebble,pebble,pebble,pebble,pebble","stone/1","basics");
        AddRecipe("stone,stone", "stone slab/2","construction");
        AddRecipe("stone slab,stone slab","ramp/1","construction");
        AddRecipe("planks,planks,planks,planks","platform/16","construction");
        AddRecipe("planks,planks,planks","stairs/2","construction");
        AddRecipe("planks,planks,stick,stick", "pickaxe/1","tools");
        AddRecipe("stick,stick,stick,stick", "frame/1","construction");
        AddRecipe("stick,stone,stone","mace/1","tools");
        AddRecipe("metal,metal,metal","beam/2","basics");
        AddRecipe("beam,beam,beam,beam","truss/1","construction");
        AddRecipe("beam,metal,stick", "sword/1","tools");
        AddRecipe("metal,stick,wires", "torch/2","basics");
        AddRecipe("gold,metal,wires,wires","processor/1","machines");
        AddRecipe("leaves,leaves,leaves,leaves,leaves", "dirt/1","other");
        
        AddRecipe("beam,metal,processor,wires,wires", "extracter/1","machines");
        AddRecipe("beam,metal,processor,torch,wires","score pump/1","machines");
        AddRecipe("beam,fan,processor,truss,wires","turbine/1","machines");
        AddRecipe("beam,leaves,processor,truss,wires","biofuel plant/1","machines");
        AddRecipe("beam,processor,qubit,truss,wires","reactor/1","machines");
        AddRecipe("battery,beam,gold,metal,wires","electric sword/1","tools");
        AddRecipe("battery,beam,gold,gold,gold","gold pick/1","tools");
    }
    private void AddRecipe(string costs, string results, string category = "default")
    {
        if(recipeBooks.ContainsKey(category))
        {
            recipeBooks[category].Add(costs,results);
        }
        else
        {
            recipeBooks.Add(category,new Dictionary<string,string>());
            recipeBooks[category].Add(costs,results);

        }
        craftingRecipes.Add(costs,results);
    }
    public static int Cnt()
    {return items.Count; }
}

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
    public static Dictionary<string,string> blockDescriptions = new Dictionary<string, string>()
    {
        {"name","Description"},
        {"wood","A useful material found in trees for constructing a variety of tools and structures"},
        {"dirt","Fairly useless substance found all over the place"},
        {"sand","Fairly useless stuff found in beaces and deserts"},
        {"pebble","Pebbles can be collected by mining stone. Combine many of them to make a stone block"},
        {"planks","Planks are a very useful material for crafting anything needing wood"},
        {"stick","Sticks can be used to craft many tools, as well as serving as a decent weapon in a pinch"},
        {"stone","Stone is crafted from pebbles, and can be used to craft heavy duty devices like a mace"},
        {"torch","Torches light up their surroundings and can also heal you when you are near them"},
        {"stone slab","Stone slabs are one-half the size of a regular block, which may come in handy for building"},
        {"ramp","Ramps can be used for slopes or to redirect extracted materials"},
        {"platform","Platforms come in large quantities and can be used to build large flat areas or bridges"},
        {"stairs","Great for mineshafts or quick ascent"},
        {"frame","Wooden structure that can absorb meteor impacts, protecting valuable parts of your base"},
        {"beam","Metal beams are vital for constructing tools and machines"},
        {"processor","Processors allow you to create all kinds of useful machines"},
        {"extracter","Extracters can extract materials out of thin air. just press E with a material in hand to tell it what to extract. Yes, I know I spelled extracter wrong but I hardcoded it into too many places to change"},
        {"score pump","Gains score for you automatically"},
        {"truss","Expensive but sturdy structure that can absorb meteor impacts. Important for constructing more complex machinery"},
        {"pickaxe","Very quick at mining things, but a weak weapon"},
        {"sword","Highly effective weapon for battling enemies"},
        {"mace","Deals huge damage, but is painfully slow to wield"},
        {"wires","Wires are usually dropped by hostile enemies, but can also be crafted with mushrooms"},
        {"turbine","Turbines passively generate score at a higher rate than score pumps"},
        {"biofuel plant","Biofuel plants require an organic material such as leaves to be input, but they generate score at a high rate"},
        {"reactor","Reactors require qubits to be input in order to run, but they generate score at an extremely high rate"},
        {"electric sword","A very powerful weapon, more powerful than a standard sword"},
        {"gold pick","A pickaxe with extremely fast mining capabilities"},
        {"extractor v2","An extractor that significantly improves extraction rates"},
        {"quantum extractor","This extractor is super fast, but requires a steady stream of qubits output from another exractor to run properly"},
        {"grenade","Grenades explode immediately upon placement, shooting shrapnel that can deal damage to enemies nearby"},
    };
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
        AddRecipe("shroom stalk,shroom stalk","planks/1","basics",4);
        AddRecipe("shroom stalk,shroom stalk","planks/1","basics",5);
        AddRecipe("planks", "stick/3","basics,construction");
        AddRecipe("pebble,pebble,pebble,pebble,pebble","stone/1","basics");
        AddRecipe("stone,stone", "stone slab/2","construction");
        AddRecipe("stone slab,stone slab","ramp/1","construction");
        AddRecipe("planks,planks,planks,planks","platform/16","construction");
        AddRecipe("planks,planks,planks","stairs/2","construction");
        AddRecipe("planks,planks,stick,stick", "pickaxe/1","tools,basics");
        AddRecipe("stick,stick,stick,stick", "frame/1","construction");
        AddRecipe("stick,stone,stone","mace/1","tools");
        AddRecipe("metal,metal,metal","beam/2","basics,tools,machines");
        AddRecipe("beam,beam,beam,beam","truss/1","construction");
        AddRecipe("beam,metal,stick", "sword/1","tools,basics");
        AddRecipe("metal,stick,wires", "torch/2","basics,tools");
        AddRecipe("blue shroom,metal,shroom","wires/1","basics,machines",4);
        AddRecipe("gold,metal,wires,wires","processor/1","machines");
        AddRecipe("leaves,leaves,leaves,leaves,leaves", "dirt/1","other");
        
        AddRecipe("beam,metal,processor,wires,wires", "extracter/1","machines");
        AddRecipe("beam,metal,processor,torch,wires","score pump/1","machines");
        AddRecipe("beam,fan,processor,truss,wires","turbine/1","machines");
        AddRecipe("beam,leaves,processor,truss,wires","biofuel plant/1","machines");
        AddRecipe("beam,processor,qubit,truss,wires","reactor/1","machines");
        AddRecipe("battery,beam,gold,metal,wires","electric sword/1","tools");
        AddRecipe("battery,beam,gold,gold,gold","gold pick/1","tools");
        AddRecipe("extracter,gold,torch,truss,wires","extractor v2/1","machines");
        AddRecipe("extracter,qubit,qubit,truss,wires","quantum extractor/1","machines");
        AddRecipe("metal,torch,wires","grenade/5","tools,machines");
    }
    private void AddRecipe(string costs, string results, string categories = "default",int levelType = -1)
    {
        if(levelType !=-1&&(int)Overlord.type != levelType) return;
        string[] categoryArr = categories.Split(",");
        foreach(string category in categoryArr)
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
        }

        craftingRecipes.Add(costs,results);
    }
    public static int Cnt()
    {return items.Count; }
}

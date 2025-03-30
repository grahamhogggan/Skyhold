using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class RecipeDisplayManager : MonoBehaviour
{
    public static int offset;
    public static string category="all";
    public static bool craftableMode = true;
    public TMP_Text txt;
    // Start is called before the first frame update
    void Start()
    {
        offset = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("w"))
        {
            Up();
        }
        if(Input.GetKeyDown("s"))
        {
            Down();
        }

        if(craftableMode)
        {
            txt.text = "\u2713";
        }
        else
        {
            txt.text = "\u2717";
        }
    }
    public void Up()
    {
            offset-=5;
            if(offset <0)
            offset = 0;
    }
    public void Down()
    {
            offset+=5;
            if(offset>=CraftingManager.RecipeCount())
            {
                offset -=5;
            }
    }
    public void ToggleCraftableMode()
    {
        craftableMode = !craftableMode;
    }
    public void SetCategory(string category)
    {
        RecipeDisplayManager.category = category;
    }
}

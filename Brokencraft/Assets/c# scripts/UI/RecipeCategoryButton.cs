using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeCategoryButton : MonoBehaviour
{
    Image img;
    [SerializeField] private string category;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(category == RecipeDisplayManager.category)
        {
            img.color = new Color(0.2122f,1,0.5441f);
        }
        else
        {
             img.color = new Color(0.6289f,1,0.5896f);
        }
    }
}

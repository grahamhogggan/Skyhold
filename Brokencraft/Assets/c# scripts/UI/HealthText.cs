using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HealthText : MonoBehaviour
{
    public static Player player;
    TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            text.text = "Health: " + player.GetHealth();
        }
    }
}

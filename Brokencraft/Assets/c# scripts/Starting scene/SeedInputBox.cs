using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SeedInputBox : MonoBehaviour
{
    private bool active;
    private bool lf;
    public static int seedChosen = 0;
    public static string seedStr = "";
    public TMP_Text seedTxt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            FirstPersonMovement.active = false;
            MouseLook.active = false;
            
            if(Input.GetMouseButtonDown(0)&&!lf)
            {
                active = false;
            }
            lf = false;
            if(Input.GetKeyDown("1")&&seedStr.Length<8)
            {
                seedStr+="1";
            }
            if(Input.GetKeyDown("2")&&seedStr.Length<8)
            {
                seedStr+="2";
            }
            if(Input.GetKeyDown("3")&&seedStr.Length<8)
            {
                seedStr+="3";
            }
            if(Input.GetKeyDown("4")&&seedStr.Length<8)
            {
                seedStr+="4";
            }
            if(Input.GetKeyDown("5")&&seedStr.Length<8)
            {
                seedStr+="5";
            }
            if(Input.GetKeyDown("6")&&seedStr.Length<8)
            {
                seedStr+="6";
            }
            if(Input.GetKeyDown("7")&&seedStr.Length<8)
            {
                seedStr+="7";
            }
            if(Input.GetKeyDown("8")&&seedStr.Length<8)
            {
                seedStr+="8";
            }
            if(Input.GetKeyDown("9")&&seedStr.Length<8)
            {
                seedStr+="9";
            }
            if(Input.GetKeyDown("0")&&seedStr.Length<8)
            {
                seedStr+="0";
            }
            if(Input.GetKeyDown(KeyCode.Backspace)&&seedStr.Length>0)
            {
                seedStr = seedStr.Substring(0,seedStr.Length-1);
            }
            if(seedStr.Length>0)
            {
                seedChosen = int.Parse(seedStr);
            }
            else
            {
                seedChosen = 0;
            }
        }
        else
        {
            FirstPersonMovement.active = true;
            MouseLook.active = true;
        }
        seedTxt.text = seedStr;
        if(seedStr.Length == 0)
        {
            seedTxt.text = "-";
        }
    }
    void OnMouseDown()
    {
        active = !active;
        lf = true;
    }
}

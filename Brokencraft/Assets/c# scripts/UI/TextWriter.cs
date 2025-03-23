using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextWriter : MonoBehaviour
{
    public static string text;
    private string targetText;
    private float charTime = 0.05f;
    private float delay = 0;
    private int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        text = "";
        targetText = "";
    }

    // Update is called once per frame
    void Update()
    {
        delay+=Time.deltaTime;
        if(delay>charTime&&index<targetText.Length)
        {
                text+=targetText[index];
                index++;
                delay = 0;
        }
        Debug.Log(text);
        Debug.Log(targetText);
    }
    public void SetTarget(string message){
        text = "";
        targetText = message;
        index = 0;
        delay =0;
    }
}

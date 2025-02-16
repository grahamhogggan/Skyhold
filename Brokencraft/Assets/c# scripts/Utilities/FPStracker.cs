using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FPStracker : MonoBehaviour
{
    TMP_Text text;
    private float totalFPS;
    private float timer;
    int cnt = 0;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer>1)
        {
            totalFPS += (1 / Time.deltaTime);
            cnt++;
            text.text = "FPS: " + (int)(1 / Time.deltaTime)+"\n Average FPS: "+(int)(totalFPS/cnt);
           
            timer = 0;
        }
    }
}

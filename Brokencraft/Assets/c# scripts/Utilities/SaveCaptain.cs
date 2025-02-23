using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveCaptain : MonoBehaviour
{
    public static int count = 0;
    private static double pace = 1;
    private static double delay;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        delay+=Time.deltaTime;
        if(delay>pace)
        {
            delay = 0;
            count++;
        }
    }
}

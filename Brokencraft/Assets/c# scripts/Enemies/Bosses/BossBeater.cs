using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class BossBeater : MonoBehaviour
{
    public static TextWriter w;
    public TextWriter globalWriter;
    private static bool waiting;
    public UnityEvent quit;
    void Start()
    {
        w = globalWriter;
        waiting = false;
    }
    public static void BeatBoss(int num)
    {
        w.SetTarget("I'm receiving some new data, come back to the ship quickly");
        switch(num)
        {
            case 1:
            PersistentData.SceneToPlay = 5;
            break;
            case 2:
            PersistentData.SceneToPlay = 6;
            break;
            case 3:
            PersistentData.SceneToPlay = 7;
            break;
            case 4:
            PersistentData.SceneToPlay = 8;
            break;
            case 5:
            PersistentData.SceneToPlay = 9;
            break;
             case 6:
            PersistentData.SceneToPlay = 10;
            break;

        }
        waiting = true;
    }
    void Update()
    {
        if(waiting)
        {
            if(w.TimeSinceFinished()>3)
            {
                quit.Invoke();
            }
        }
    }
}

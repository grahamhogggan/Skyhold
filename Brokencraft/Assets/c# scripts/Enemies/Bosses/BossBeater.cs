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
        if (num == 8)
        {
            w.SetTarget("Incredible job down there, there's something you need to see, now");
        }
        else if(num == 9)
        {
            w.SetTarget("!!! Attention all spectators, we have had an emergency, please exit the stadium as efficiently as possible !!!");
        }
        else
        {
            w.SetTarget("Great job. Come back to the ship, I want to tell you something");

        }
        switch (num)
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
            case 7:
                PersistentData.SceneToPlay = 11;
                break;
            case 8:
                PersistentData.SceneToPlay = 12;
                break;
            case 9:
                PersistentData.SceneToPlay = 14;
                break;

        }
        waiting = true;
    }
    void Update()
    {
        if (waiting)
        {
            if (w.TimeSinceFinished() > 3)
            {
                quit.Invoke();
            }
        }
    }
}

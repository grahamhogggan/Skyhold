using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PersistentData
{
    public static bool[] bossesCleared;
    public static int[] highScores;

    public static int SceneToPlay;
    public static int tutorialStep;
    public static persistentDataStruct getSaveData()
    {
        persistentDataStruct data = new persistentDataStruct();
        data.bossesCleared = bossesCleared;
        data.highScores = highScores;
        data.sceneToPlay = SceneToPlay;
        data.tutorialStep = tutorialStep;
        return data;
    }
    public static void Init(persistentDataStruct s)
    {
        bossesCleared = s.bossesCleared;
        highScores = s.highScores;
        SceneToPlay = s.sceneToPlay;
        tutorialStep = s.tutorialStep;
    }
    public static void Init(int numLevels)
    {
        bossesCleared = new bool[numLevels+1];
        highScores = new int[numLevels];
        SceneToPlay = 1;
        tutorialStep = 1;
    }
}
[System.Serializable]

public struct persistentDataStruct
{
    public bool[] bossesCleared;
    public int[] highScores;

    public int sceneToPlay;
    public int tutorialStep;
}

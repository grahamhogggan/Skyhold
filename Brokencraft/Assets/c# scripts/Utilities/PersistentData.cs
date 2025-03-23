using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PersistentData
{
    public static bool[] bossesCleared;
    public static int[] highScores;

    public static int playedScenes = 0;
    public static persistentDataStruct getSaveData()
    {
        persistentDataStruct data = new persistentDataStruct();
        data.bossesCleared = bossesCleared;
        data.highScores = highScores;
        data.playedScenes = playedScenes;
        return data;
    }
    public static void Init(persistentDataStruct s)
    {
        bossesCleared = s.bossesCleared;
        highScores = s.highScores;
        playedScenes = s.playedScenes;
    }
    public static void Init(int numLevels)
    {
        bossesCleared = new bool[numLevels+1];
        highScores = new int[numLevels];
        playedScenes = 0;
    }
}
[System.Serializable]

public struct persistentDataStruct
{
    public bool[] bossesCleared;
    public int[] highScores;

    public int playedScenes;
}

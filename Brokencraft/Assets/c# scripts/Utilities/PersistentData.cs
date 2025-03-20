using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PersistentData
{
    public static bool[] bossesCleared;
    public static int[] highScores;
    public static persistentDataStruct getSaveData()
    {
        persistentDataStruct data = new persistentDataStruct();
        data.bossesCleared = bossesCleared;
        data.highScores = highScores;
        return data;
    }
    public static void Init(persistentDataStruct s)
    {
        bossesCleared = s.bossesCleared;
        highScores = s.highScores;
    }
    public static void Init(int numLevels)
    {
        bossesCleared = new bool[numLevels+1];
        highScores = new int[numLevels];
    }
}
[System.Serializable]

public struct persistentDataStruct
{
    public bool[] bossesCleared;
    public int[] highScores;
}

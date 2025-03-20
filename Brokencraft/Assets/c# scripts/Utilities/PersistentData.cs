using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PersistentData
{
    public static bool[] bossesCleared;
    public static persistentDataStruct getSaveData()
    {
        persistentDataStruct data = new persistentDataStruct();
        data.bossesCleared = bossesCleared;
        return data;
    }
    public static void Init(persistentDataStruct s)
    {
        bossesCleared = s.bossesCleared;
    }
    public static void Init(int numLevels)
    {
        bossesCleared = new bool[numLevels+1];
    }
}
[System.Serializable]

public struct persistentDataStruct
{
    public bool[] bossesCleared;
}

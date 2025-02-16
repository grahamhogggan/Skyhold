using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class ScoreManager : MonoBehaviour
{
    private static  int score;
    private static string savePath;
    // Start is called before the first frame update
    void Start()
    {
        savePath = Application.persistentDataPath+"/score_data";
        if((!Overlord.OverwriteMode)&&File.Exists(savePath)&&Int32.TryParse(File.ReadAllText(savePath), out int res))
        {
        score = res;

        }
        else
        {
            score = 0;
        }
        Save();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void AddScore(int number)
    {
        score+=number;
        Save();
    }
    public static int GetScore()
    {
        return score;
    }
    private static void Save()
    {
        string save = score.ToString();
        File.WriteAllText(savePath,save);
    }
}

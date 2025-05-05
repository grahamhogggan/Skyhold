using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class ScoreManager : MonoBehaviour
{
    private static int totalScore;
    private static int activeScore;
    private static string savePath;
    // Start is called before the first frame update
    void Start()
    {
        savePath = Overlord.saveDirectory+"/score_data";
        if((!Overlord.OverwriteMode)&&File.Exists(savePath))
        {
            ScoreSave scores = JsonUtility.FromJson<ScoreSave>(Encryption.Decrypt(File.ReadAllText(savePath)));
        totalScore = scores.totalScore;
        activeScore = scores.activeScore;

        }
        else
        {
            totalScore = 0;
            activeScore = 0;
        }
        SaveManager.Register(Save);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void AddScore(int number)
    {
        totalScore+=number;
        activeScore+=number;
        
    }
    public static int GetScore()
    {
        return totalScore;
    }
    public static int GetActiveScore()
    {
        return activeScore;
    }
    public static void SpendScore(int amount)
    {
        activeScore-=amount;
    }
    private static void Save()
    {
        string save = JsonUtility.ToJson(new ScoreSave(totalScore,activeScore));
        save = Encryption.Encrypt(save);
        File.WriteAllText(savePath,save);
    }
}
 class ScoreSave
{
    public int totalScore;
    public int activeScore;
    public ScoreSave(int total, int active)
    {
        totalScore = total;
        activeScore = active;
    }
}

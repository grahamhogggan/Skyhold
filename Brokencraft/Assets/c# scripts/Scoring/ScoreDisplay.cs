using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreDisplay : MonoBehaviour
{
    private TMP_Text text;
    public bool newLine = false;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
        UpdateHighScores();
    }

    // Update is called once per frame
    void Update()
    {
        if(newLine)
        {
        text.text ="Score: "+ScoreManager.GetScore()+"\nEnergy: "+ScoreManager.GetActiveScore();

        }
        else
        {
         text.text ="Score: "+ScoreManager.GetScore()+"   Energy: "+ScoreManager.GetActiveScore();
        }
    }
    public static void UpdateHighScores()
    {
        int currentHighScore = PersistentData.highScores[(int)Overlord.type];
        int latestScore = ScoreManager.GetScore();
        if(latestScore>currentHighScore)
        {
             PersistentData.highScores[(int)Overlord.type] = latestScore;
        }
    }
}

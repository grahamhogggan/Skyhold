using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewGameManager : MonoBehaviour
{
    public GameObject[] newGameButtons;
    public TMP_Text[] highScoreTexts;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i<newGameButtons.Length;i++)
        {
            if(PersistentData.bossesCleared[i]||i==0)
            {
                newGameButtons[i].SetActive(true);
            }
            else
            {
                newGameButtons[i].SetActive(false);
            }
            highScoreTexts[i].text = "High Score: "+PersistentData.highScores[i];
        }
    }
}

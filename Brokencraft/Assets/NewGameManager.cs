using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameManager : MonoBehaviour
{
    public GameObject[] newGameButtons;
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
        }
    }
}

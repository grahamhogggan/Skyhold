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
    }

    // Update is called once per frame
    void Update()
    {
        if(newLine)
        {
        text.text ="Score: \n"+ScoreManager.GetScore();

        }
        else
        {
        text.text ="Score: "+ScoreManager.GetScore();

        }
    }
}

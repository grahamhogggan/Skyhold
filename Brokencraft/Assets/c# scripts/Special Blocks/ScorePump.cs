using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePump : MonoBehaviour
{
    [SerializeField] private double sps;
    private float delay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        delay+=Time.deltaTime;
        if(sps>1)
        {
        if(delay>1)
        {
            delay = 0;
            ScoreManager.AddScore((int)sps);
        }
        }
        else
        {
            if(delay>1/sps)
            {
                delay = 0;
                ScoreManager.AddScore(1);
            }
        }

    }
}

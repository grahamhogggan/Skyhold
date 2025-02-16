using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdentifier : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        Enemy.target = gameObject;
        HealthText.player = GetComponent<Player>();
        Overlord.player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

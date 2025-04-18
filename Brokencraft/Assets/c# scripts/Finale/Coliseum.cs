using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coliseum : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Overlord.type != Overlord.WorldType.Finale) Destroy(gameObject);
    }
}

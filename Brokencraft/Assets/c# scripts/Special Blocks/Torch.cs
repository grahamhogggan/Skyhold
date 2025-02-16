using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    [SerializeField] private float wait = 2;
    [SerializeField] private float distance = 3;
    private float delay = 0;
    // Start is called before the first frame update
    void Start()
    {
        delay = 0;
    }

    // Update is called once per frame
    void Update()
    {
        delay+=Time.deltaTime;
        if(delay>wait)
        {
            delay = 0;
            if(Vector3.Distance(transform.position, Overlord.player.transform.position)<=distance)
            {
            Overlord.player.Heal(1);

            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    public void Slide()
    {
        GetComponent<Animator>().SetBool("open",!GetComponent<Animator>().GetBool("open"));
    }
}

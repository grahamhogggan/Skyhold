using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateElevator : MonoBehaviour
{
    public Animator anim;
    public void Run()
    {
        anim.SetTrigger("down");
    }
}

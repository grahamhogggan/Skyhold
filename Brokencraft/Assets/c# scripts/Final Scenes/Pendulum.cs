using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    public AudioSource src1;
    public AudioSource src2;
    public void SwingLeft()
    {
        src1.Play();
    }
    public void SwingRight()
    {
        src2.Play();
    }
}

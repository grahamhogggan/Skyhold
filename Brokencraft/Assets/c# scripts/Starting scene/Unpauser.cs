using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unpauser : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        MouseLook.active = true;
    }
}

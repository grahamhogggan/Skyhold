using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enabler : MonoBehaviour
{
    public GameObject[] g;
    void E(int itemNumber)
    {
        g[itemNumber].SetActive(true);
    }
    void D(int itemNumber)
    {
        g[itemNumber].SetActive(false);
    }
}

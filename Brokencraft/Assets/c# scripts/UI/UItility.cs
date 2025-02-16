using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UItility : MonoBehaviour
{
    public void Toggle(GameObject obj)
    {
        obj.SetActive(!obj.activeSelf);
    }
}

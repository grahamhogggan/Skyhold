using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UItility : MonoBehaviour
{
    public void Toggle(GameObject obj)
    {
        obj.SetActive(!obj.activeSelf);
    }
    public void TurnOn(GameObject obj)
    {
        obj.SetActive(true);
    }
        public void TurnOFf(GameObject obj)
    {
        obj.SetActive(false);
    }
}

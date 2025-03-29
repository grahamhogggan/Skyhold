using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    public LayerMask hitlayers;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position,transform.forward,out hit,Mathf.Infinity,hitlayers))
            {
                GameObject objectHit = hit.transform.gameObject;
                if(objectHit.GetComponent<Clickable>())
                {
                    objectHit.GetComponent<Clickable>().OnClick.Invoke();
                    AudioSource sound = objectHit.GetComponent<Clickable>().interactSound;
                    if(sound!=null) sound.Play();
                }
            }
        }
    }
}

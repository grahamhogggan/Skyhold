using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEnter : MonoBehaviour
{
    public UnityEvent Trigger;
    void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.GetComponent<FirstPersonMovement>())
        {
            Trigger.Invoke();
        }
    }
}

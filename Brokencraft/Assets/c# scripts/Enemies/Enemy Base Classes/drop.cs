using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drop : MonoBehaviour
{
    CharacterController c;    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        c = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        c.Move(Vector3.up*-speed*Time.deltaTime);
    }
}

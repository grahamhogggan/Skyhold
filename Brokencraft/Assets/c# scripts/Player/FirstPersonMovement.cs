using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private CharacterController controller;
    public float Vvelocity;
    public float gravity;

    public bool inWater = false;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float dirX = Input.GetAxisRaw("Horizontal") * speed*Time.deltaTime;
        float dirZ = Input.GetAxisRaw("Vertical") * speed*Time.deltaTime;
        controller.Move(transform.TransformDirection(dirX, Vvelocity*Time.deltaTime,dirZ));

        if (Input.GetKey("space")&&controller.isGrounded)
        {
            Vvelocity = jumpForce;
        }
        if(Vvelocity > gravity)
        {
            Vvelocity += gravity*Time.deltaTime;
        }
        if(inWater&&Vvelocity<1)
        {
            Vvelocity += 30*Time.deltaTime ;
        }
    }
}

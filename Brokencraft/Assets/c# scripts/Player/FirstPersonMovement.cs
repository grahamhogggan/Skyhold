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
    public float fallDamageThreshold;

    public bool inWater = false;
    private bool inAir = false;
    public static bool active = true;
    public AudioSource footsteps;
    public AudioSource jump;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        float dirX = active ? Input.GetAxisRaw("Horizontal") * speed : 0;
        float dirZ = active ? Input.GetAxisRaw("Vertical") * speed : 0;
        Vector3 movement = transform.TransformDirection(dirX, Vvelocity, dirZ);
        if (Vector3.Magnitude(new Vector3(dirX, 0, dirZ)) > 0.1f && controller.isGrounded)
        {
            footsteps.enabled = true;
        }
        else
        {
            footsteps.enabled = false;
        }
        controller.Move(movement*Time.deltaTime);

        if (controller.isGrounded)
        {
            if (inAir == true)
            {
                if (Mathf.Abs(Vvelocity) > fallDamageThreshold)
                {
                    GetComponent<Player>().Damage((int)Mathf.Abs(Vvelocity));
                }
                inAir = false;
            }
            if (Vvelocity < -1)
            {
                Vvelocity = -1;

            }
        }
        else
        {
            inAir = true;
        }
        if (Input.GetKey("space") && controller.isGrounded && active)
        {
            Vvelocity = jumpForce;
            jump.Play();
        }
        if (Vvelocity > -100)
        {
            Vvelocity += gravity * Time.deltaTime;
        }
        if (inWater && Vvelocity < 1)
        {
            Vvelocity += 30 * Time.deltaTime;
        }
        if (inWater && Vvelocity < -8)
        {
            Vvelocity = -8;
        }


    }
}

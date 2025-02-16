using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemy : Enemy
{
    public CharacterController body;
    public float speed;
    public bool inWater = false;
    private float Yvelocity;
    [SerializeField] private Transform feet;
    private bool jumping;
    [SerializeField] private int strength;
    [SerializeField] private float attackSpeed;
    [SerializeField] private GameObject jetPackFlames;
    [SerializeField] private Transform eyeballs;
    private float haltTimer;
    public enum State
    {
        chasing = 1,
        retreating = 2,
        halting = 3,
    };
    private State currentState;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        body = GetComponent<CharacterController>();
        currentState = State.chasing;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState == State.halting)
        {
            haltTimer += Time.deltaTime;
        }
        if(haltTimer > attackSpeed)
        {
            haltTimer = 0;
            currentState = State.chasing;
        }
        base.Update();
        if(inWater&&Yvelocity<3f)
        {
            Yvelocity+=Time.deltaTime*10;
        }
        else if (!inWater &&Yvelocity>-9.8f&&!jumping)
        {
            Yvelocity -= Time.deltaTime*10;
        }

        body.Move(new Vector3(0, Yvelocity, 0) * Time.deltaTime);

        if(CanSeeTarget(eyeballs))
        {
            Move();
        }
        
    }
    void Move()
    {
        Vector3 playerLocator = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        transform.LookAt(playerLocator);    
        Vector3 dir;
        if(currentState==State.chasing)
        {
            dir = target.transform.position - transform.position;
        }
        else
        {
            dir = transform.position-target.transform.position;
        }
        body.Move(new Vector3(dir.x, 0, dir.z) * speed * Time.deltaTime);

        if(ShouldJump(dir))
        {
            jumping = true;
            Yvelocity = 5;
            jetPackFlames.SetActive(true);
        }
        else
        {
            jetPackFlames.SetActive(false);
            jumping = false;
        }
    }
    void OnTriggerEnter(Collider coll)
    {
        Player player = coll.gameObject.GetComponent<Player>();
        if(player!=null)
        {
            player.Damage(strength);
            currentState = State.retreating;
        }
    }
    void OnTriggerExit(Collider coll)
    {
        Player player = coll.gameObject.GetComponent<Player>();
        if (player != null)
        {
            haltTimer = 0;
            currentState = State.halting;
        }
    }
    private bool ShouldJump(Vector3 dir)
    {
        RaycastHit hit;
        if(Physics.Raycast(feet.transform.position,new Vector3(dir.x,0,dir.z),out hit,1)&&!Physics.Raycast(transform.position, transform.up, 2)&&body.isGrounded)
        {
            if(hit.transform.gameObject.GetComponent<Player>())
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        return false;
    }
}

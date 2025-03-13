using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Turret : Turret
{
    public Animator anim;
    public bool Shoot(Transform target)
    {
        if(base.Shoot(target))
        {
                    anim.SetBool("Raised",!anim.GetBool("Raised"));
            return true;
        }
        return false;
    }
}

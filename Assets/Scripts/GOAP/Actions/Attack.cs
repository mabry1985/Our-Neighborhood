using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : GAction
{
    ChatPlayerController player;
    float weaponDamage = 2f;

    public override bool PrePerform()
    {
        return true;
    }
    public override bool PostPerform()
    {
        return true;
    }

    //animation event
    void Hit()
    {
        Health target = GetComponent<Health>();
        if (target == null) return;
        target.TakeDamage(weaponDamage);
    }
}
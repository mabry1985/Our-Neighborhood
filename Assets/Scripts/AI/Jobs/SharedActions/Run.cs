using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : GAction
{
    float originalSpeed;

    private void Start() {
        originalSpeed = navMeshAgent.speed;
    }

    public override bool PrePerform()
    {
        navMeshAgent.speed = originalSpeed * 2f;
        return true;
    }

    public override bool PostPerform()
    {
        navMeshAgent.speed = originalSpeed;
        this.GetComponent<PlayerGAgent>().RemoveFear();
        return true;
    }
}

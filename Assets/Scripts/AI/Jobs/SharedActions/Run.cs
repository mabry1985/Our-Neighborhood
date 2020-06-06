using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : GAction
{
    float originalSpeed;

    private void Start() {
        originalSpeed = agent.speed;
    }

    public override bool PrePerform()
    {
        agent.speed = originalSpeed * 2f;
        return true;
    }

    public override bool PostPerform()
    {
        agent.speed = originalSpeed;
        this.GetComponent<GAgent>().beliefs.ModifyState("inDanger", -1);
        return true;
    }
}

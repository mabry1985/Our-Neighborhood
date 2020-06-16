using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FindFriend : GAction
{
    public override bool PrePerform()
    {
        return true;
    }
    public override bool PostPerform()
    {
        navMeshAgent.GetComponent<GAgent>().beliefs.SetState("hasFriend", 1);
        return true;
    }
}

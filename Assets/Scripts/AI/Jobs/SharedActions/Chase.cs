using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : GAction
{
    public override bool PrePerform()
    {
        return true;
    }
    public override bool PostPerform()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.ResetPath();
        return true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : GAction
{

    private void LateUpdate() 
    {   

        //if(target != null)
            //navMeshAgent.SetDestination(target.transform.position);
    }

    public override bool PrePerform()
    {
        if (GetComponent<GAgent>().distanceToTarget > this.range)
        {
            GetComponent<GAgent>().beliefs.ModifyState("inRange", 1);
        }
        print("in chase preperform");
        return true;
    }

    public override bool PostPerform()
    {
        print("in chase postperform");
        navMeshAgent.isStopped = true;
        navMeshAgent.ResetPath();
        return true;
    }
}

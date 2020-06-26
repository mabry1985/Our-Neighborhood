using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : GAction
{
    GAgent gAgent;

    private void Start() 
    {
        gAgent = GetComponent<GAgent>();    
    }

    private void Update() 
    {
        // if(target != null && Vector3.Distance(transform.position, target.transform.position) > this.range)
        // {
        //     print(target.name);
        //     navMeshAgent.SetDestination(target.transform.position);
        // }
        // else
        // {
        //     gAgent.CancelGoap();

        //     if (navMeshAgent.enabled == true)
        //         navMeshAgent.ResetPath();
            
        //     //gAgent.beliefs.ModifyState("inRange", 1);
        // }
    }

    public override bool PrePerform()
    {
        print("in chase preperform");
        return true;
    }

    public override bool PostPerform()
    {
        print("in chase postperform");
        if (GetComponent<Health>().IsDead()) return false;

        navMeshAgent.isStopped = true;
        navMeshAgent.ResetPath();
        return true;
    }
}

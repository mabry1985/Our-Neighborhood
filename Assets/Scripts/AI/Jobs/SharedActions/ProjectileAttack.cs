using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ProjectileAttack : GAction
{
    Transform projectileSpawnPoint;
    public GameObject projectilePrefab;
    // [SerializeField] float projectileRange = 10f;
    GAgent gAgent;

    private void Start() 
    {
        projectileSpawnPoint = GetComponentInChildren<ProjectileSpawnPoint>().transform ?? null;
        gAgent = GetComponent<GAgent>();
    }

    public override bool PrePerform()
    {   
        if (gAgent.distanceToTarget > this.range) 
        {
            gAgent.beliefs.RemoveState("inRange");
            return false;
        } 
        return true;
    }

    public override bool PostPerform()
    {
        HandleAttack();
        ResetAgentAttack();

        return true;
    }
    
    private void HandleAttack()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        transform.LookAt(target.transform);
        GetComponent<Animator>().SetTrigger("projectileAttack");
    }

    private void ResetAgentAttack()
    {
        gAgent.timeSinceLastAttack = 0;
        gAgent.beliefs.RemoveState("canAttack");
    }

    //animation event
    void LaunchProjectile()
    {
        Health target = GetComponent<Health>();
        if (target == null) return;
        Instantiate(projectilePrefab, projectileSpawnPoint.position, transform.rotation);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, this.range);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ProjectileAttack : GAction
{
    float weaponDamage = 2f;
    Transform projectileSpawnPoint;
    public GameObject projectilePrefab;
    // [SerializeField] float projectileRange = 10f;

    private void Start() 
    {
        projectileSpawnPoint = GetComponentInChildren<ProjectileSpawnPoint>().transform ?? null;
    }

    public override bool PrePerform()
    {   
        if (GetComponent<GAgent>().distanceToTarget > this.range) 
        {
            GetComponent<GAgent>().beliefs.RemoveState("inRange");
            return false;
        }

        return true;
    }

    public override bool PostPerform()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        FaceTarget(target.transform.position);
        GetComponent<Animator>().SetTrigger("projectileAttack");
        return true;
    }

    //animation event
    void LaunchProjectile()
    {
        Health target = GetComponent<Health>();
        if (target == null) return;
        Instantiate(projectilePrefab, projectileSpawnPoint.position, transform.rotation);
    }

    public void FaceTarget(Vector3 destination)
    {
        Vector3 lookPos = destination - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, this.range);
    }
}
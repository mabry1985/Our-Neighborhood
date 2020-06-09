using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wander : GAction
{
    [SerializeField] float wanderRadius = 50f;

    public override bool PrePerform()
    {
        return true;
    }

    public override bool PostPerform()
    {
        Wandering();
        return true;
    }

    private void Wandering()
    {
        Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
        GetComponentInChildren<TravelPoint>().transform.position = newPos;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, wanderRadius);
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MakeSpace : GAction
{
    [SerializeField] float wanderRadius = 50f;

    public override bool PrePerform()
    {
        return true;
    }

    public override bool PostPerform()
    {
        MakingSpace();
        return true;
    }

    private void MakingSpace()
    {
        Vector3 travelPoint = GetComponentInChildren<TravelPoint>().transform.position;
        Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
        GetComponentInChildren<TravelPoint>().transform.position = newPos;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, wanderRadius);
    }

}
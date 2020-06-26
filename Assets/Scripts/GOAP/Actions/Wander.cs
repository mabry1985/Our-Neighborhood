using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wander : GAction
{
    [SerializeField] float wanderRadius = 50f;


    private void Start() 
    {
        target = GetComponentInChildren<TravelPoint>().gameObject;
    }

    public override bool PrePerform()
    {
        print("in wandering pre perform" + target);
        return true;
    }

    public override bool PostPerform()
    {
        Wandering();
        print("in wandering post perform");
        return true;
    }

    private void Wandering()
    {
        Vector3 travelPoint = target.transform.position;
        Vector3 newPos = RandomNavSphere(travelPoint, wanderRadius, -1);
        travelPoint = newPos;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, wanderRadius);
    }

}
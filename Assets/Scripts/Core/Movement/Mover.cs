using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour, IAction
{

    private NavMeshAgent navAgent;
    Health health;

    private void Start()
    {
        health = GetComponent<Health>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        navAgent.enabled = !health.IsDead();
        UpdateAnimator();
    }

    public void StartMoveAction(Vector3 destination)
    {
        GetComponent<ActionScheduler>().StartAction(this);
        MoveTo(destination);

    }
    public void MoveTo(Vector3 destination)
    {
        navAgent.isStopped = false;
        navAgent.SetDestination(destination);
        //navAgent.u
    }

    public void Cancel()
    {
        navAgent.isStopped = true;
    }

    private void UpdateAnimator()
    {
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        GetComponent<Animator>().SetFloat("forwardSpeed", speed);
    }
}

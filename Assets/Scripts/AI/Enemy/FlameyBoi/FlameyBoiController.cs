using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameyBoiController : MonoBehaviour
{
    [SerializeField] float chaseDistance = 5f;
    [SerializeField] float suspicionTime = 3f;
    [SerializeField] PatrolPath patrolPath;
    [SerializeField] float waypointTolerance = 3f;
    [SerializeField] float waypointPauseTime = 3f;

    Fighter fighter;
    GameObject[] players;
    GameObject targetPlayer;
    Mover mover;
    Health health;
    ParticleSystem particle;

    Vector3 guardPosition;
    float timeSinceLastSawPlayer = Mathf.Infinity;
    float timeSinceArrivedAtWaypoint = Mathf.Infinity;
    int currentWaypointIndex = 0;

    private void Start() {
        health = GetComponent<Health>();
        fighter = GetComponent<Fighter>();
        mover = GetComponent<Mover>();
        particle = GetComponentInChildren<ParticleSystem>();
        guardPosition = transform.position;
    }

    private void Update()
    {
        if (health.IsDead())
        {
            particle.Stop();
            return;
        } 

        players = GameObject.FindGameObjectsWithTag("Player");
        targetPlayer = GetClosestEnemy(players).gameObject;
        print(targetPlayer.name);

        if (InAttackRange() && fighter.CanAttack(targetPlayer))
        {
            AttackBehaviour();
        }
        else if (timeSinceLastSawPlayer < suspicionTime)
        {
            SuspicionBehaviour();
        }
        else
        {
            PatrolBehaviour();
        }

        UpdateTimers();
    }

    private void UpdateTimers()
    {
        timeSinceLastSawPlayer += Time.deltaTime;
        timeSinceArrivedAtWaypoint += Time.deltaTime;
    }

    private void PatrolBehaviour()
    {
        Vector3 nextPosition = guardPosition;

        if (patrolPath != null)
        {
            if (AtWaypoint())
            {
                timeSinceArrivedAtWaypoint = 0;
                CycleWaypoint();
            }
            nextPosition = GetCurrentWaypoint();
        }
        if(timeSinceArrivedAtWaypoint > waypointPauseTime)
        {
            mover.StartMoveAction(nextPosition);
        }
    }

    private bool AtWaypoint()
    {
        float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
        return distanceToWaypoint < waypointTolerance;
    }

    private void CycleWaypoint()
    {
        currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
    }

    private Vector3 GetCurrentWaypoint()
    {
        return patrolPath.GetWaypoint(currentWaypointIndex);
    }

    private void SuspicionBehaviour()
    {
        GetComponent<ActionScheduler>().CancelCurrentAction();
    }

    private void AttackBehaviour()
    {
        timeSinceLastSawPlayer = 0;
        fighter.Attack(targetPlayer);
    }

    private bool InAttackRange()
    {
        float distanceToPlayer = Vector3.Distance(targetPlayer.transform.position, transform.position);
        print(targetPlayer.name + distanceToPlayer);
        return distanceToPlayer < chaseDistance;
    }

    Transform GetClosestEnemy(GameObject[] enemies)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (GameObject potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget.transform;
            }
        }

        return bestTarget;
    }

    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }
}

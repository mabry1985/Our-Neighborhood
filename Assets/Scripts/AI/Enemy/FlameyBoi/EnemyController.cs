using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float suspicionTime = 3f;
    [SerializeField] float wanderRadius = 10f;
    [SerializeField] float wanderingPauseTime = 2f;

    GameObject[] players;
    protected Health health;
    ParticleSystem particle;

    protected void Start() {
        health = GetComponent<Health>();
        particle = GetComponentInChildren<ParticleSystem>();
        //StartCoroutine(Wander());
    }

    private void Update()
    {
        if (health.IsDead())
        {
            if (particle == null) return;

            particle.Stop();  
            return;
        } 
    }

    public Transform GetClosestEnemy(List<GameObject> enemies)
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

    IEnumerator Wander()
    {
        while (true)
        {
            yield return new WaitForSeconds(wanderingPauseTime);
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            if (!GetComponent<Health>().IsDead())
                GetComponent<NavMeshAgent>().SetDestination(newPos);
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, wanderRadius);
    }
}

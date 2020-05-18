using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Idle : MonoBehaviour
{
    public NavMeshAgent agent;

    void Awake()
    {
        StartCoroutine(Wander());
    }

    IEnumerator Wander()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            Vector3 newPos = RandomNavSphere(transform.position, 5, -1);
            agent.SetDestination(newPos);
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
}

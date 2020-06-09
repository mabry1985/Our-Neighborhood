using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    GameObject[] players;
    protected Health health;
    ParticleSystem particle;

    protected void Start() {
        health = GetComponent<Health>();
        particle = GetComponentInChildren<ParticleSystem>();
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

}

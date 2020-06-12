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

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Health : MonoBehaviour
{
    [SerializeField] float health = 100f;
    float originalHealth;

    bool isDead = false;
    Player player;
    NavMeshAgent navMeshAgent;
    Animator animator;
    [SerializeField] Transform home; 

    private void Start() 
    {
        originalHealth = health;
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
    }

    public bool IsDead()
    {
        return isDead;
    }

    public void TakeDamage(float damage)
    {
        health = Mathf.Max(health - damage, 0);
        if (health == 0)
            Die();
    }

    public void Die()
    {
        if (isDead) return;
        
        isDead = true;
        
        ActionScheduler actionScheduler = GetComponent<ActionScheduler>();
        if (actionScheduler != false)
            GetComponent<ActionScheduler>().CancelCurrentAction();
        
        navMeshAgent.enabled = false;
        animator.enabled = false;

        if (player == null) return;
        
        Invoke("OnRevive", 10);
        
        Canvas nameCanvas = player.playerNameCanvas;

        if (nameCanvas == null) return;

        nameCanvas.enabled = false;
    }

    public void OnRevive()
    {
        navMeshAgent.transform.position = home.position;
        isDead = false;
        navMeshAgent.enabled = true;
        navMeshAgent.isStopped = false;
        animator.enabled = true;
        health = originalHealth;   

        Canvas nameCanvas = player.playerNameCanvas;

        if (nameCanvas == null) return;

        nameCanvas.enabled = true;

    }
}


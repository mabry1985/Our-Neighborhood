using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Health : MonoBehaviour
{
    [SerializeField] float health = 100f;
    bool isDead = false;
    Player player;
    NavMeshAgent navMeshAgent;
    Animator animator;

    private void Start() 
    {
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
        GetComponent<ActionScheduler>().CancelCurrentAction();
        navMeshAgent.enabled = false;
        animator.enabled = false;

        // if (this.tag == "Streamer")
        // {
        //     //var cam = player.playerController.vcam;
        //     //cam.m_Follow = null;       
        // }

        if (player == null) return;
        
        Invoke("OnRevive", 10);
        
        Canvas nameCanvas = player.playerNameCanvas ?? null;

        if (nameCanvas == null) return;

        nameCanvas.enabled = false;
    }

    public void OnRevive()
    {
        navMeshAgent.transform.position = player.home.position;
        isDead = false;
        navMeshAgent.enabled = true;
        navMeshAgent.isStopped = false;
        animator.enabled = true;
        

        // if (this.tag == "Streamer")
        // {
        //     //var cam = player.playerController.vcam;
        //     // cam.m_Follow = this.transform;
        // }
        Canvas nameCanvas = player.playerNameCanvas;

        if (nameCanvas == null) return;

        nameCanvas.enabled = true;

    }
}

// public class Health : MonoBehaviour
// {
//     [SerializeField] float health = 100f;
//     float originalHealth;
//     bool isDead = false;
//     Player player;
//     NavMeshAgent navMeshAgent;
//     Animator animator;

//     private void Start()
//     {
//         player = GetComponent<Player>();
//         originalHealth = health;
//         navMeshAgent = GetComponent<NavMeshAgent>();
//         animator = GetComponent<Animator>();
//     }

//     public bool IsDead()
//     {
//         return isDead;
//     }

//     public void TakeDamage(float damage)
//     {
//         health = Mathf.Max(health - damage, 0);
//         if (health == 0)
//             Die();
//     }

//     public void Die()
//     {
//         if (isDead) return;

//         isDead = true;

//         if (player == null) return;
//         GetComponent<ActionScheduler>().CancelCurrentAction();
//         navMeshAgent.enabled = false;
//         animator.enabled = false;

//         Invoke("Respawn", 10);

//         Canvas nameCanvas = player.playerNameCanvas;

//         if (nameCanvas == null) return;

//         nameCanvas.enabled = false;

//     }

//     public void Respawn()
//     {

//         navMeshAgent.transform.position = player.home.position;

//         isDead = false;
//         navMeshAgent.enabled = true;
//         navMeshAgent.isStopped = false;
//         animator.enabled = true;

//         Canvas nameCanvas = player.playerNameCanvas;

//         if (nameCanvas == null) return;

//         nameCanvas.enabled = true;

//     }
// }

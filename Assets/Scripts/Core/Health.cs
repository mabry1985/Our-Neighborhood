using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Health : MonoBehaviour
{
    [SerializeField] float health = 100f;
    bool isDead = false;
    //Player player;
    NavMeshAgent navMeshAgent;
    Animator animator;

    private void Start() 
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
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

    // private void Die()
    // {

    //     isDead = true;
    //     GetComponent<Animator>().SetTrigger("die");
    //     GetComponent<ActionScheduler>().CancelCurrentAction();
    //     //GetComponent<CapsuleCollider>().enabled = false;

    // }

    public void Die()
    {
        if (isDead) return;

        //player.playerNameCanvas.enabled = false;
        isDead = true;
        GetComponent<ActionScheduler>().CancelCurrentAction();
        navMeshAgent.enabled = false;
        animator.enabled = false;

        if (this.tag == "Streamer")
        {
            //var cam = player.playerController.vcam;
            //cam.m_Follow = null;       
        }

        if (this.tag == "Player")
        {
            Invoke("OnRevive", 10);
        }
    }

    public void OnRevive()
    {
        Player player = GetComponent<Player>();
        if(player == null ) return;

        navMeshAgent.transform.position = player.home.position;
        
        isDead = false;
        //player.playerNameCanvas.enabled = true;
        navMeshAgent.enabled = true;
        navMeshAgent.isStopped = false;
        animator.enabled = true;

        

        if (this.tag == "Streamer")
        {
            //var cam = player.playerController.vcam;
            // cam.m_Follow = this.transform;
        }

    }
}

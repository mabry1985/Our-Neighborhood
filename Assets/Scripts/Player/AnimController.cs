using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimController : MonoBehaviour
{
    public bool isStanding = true;
    public bool isFollowing = false;
    public bool isChopping = false;
    Animator animator;
    NavMeshAgent navAgent;

    private void Start() 
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
    }
    
    private void Update()
    {
        UpdateAnimator();
    }

    public void FarmAnimHandler(Animator animator, string material)
    {
        switch (material)
        {
            case "Stone":
                animator.SetBool("isMining", true);
                break;
            case "Wood":
                animator.SetBool("isChopping", true);
                break;
            case "Wheat":
                animator.SetBool("isGathering", true);
                break;
            case "Water":
                animator.SetBool("isGathering", true);
                break;
        }
    }

    public void CancelAnimations(Animator animator)
    {
        animator.SetBool("isMining", false);

        animator.SetBool("isChopping", false);
        
        animator.SetBool("isGathering", false);
        
        animator.SetBool("isStanding", true);
    }

    public void SitDown()
    {
        if (isStanding)
        {
            //animator.SetFloat("speedPercent", 0.0f);
            animator.SetBool("isSittingGround", true);
            animator.SetBool("isStanding", false);
            navAgent.enabled = false;
            isStanding = false;
        }
        else
        {
            animator.SetBool("isSittingGround", false);
            animator.SetBool("isStanding", true);
            isStanding = true;
        }
    }

    public void WaveHello()
    {
        animator.SetBool("isWaving", true);
        navAgent.enabled = false;
    }

    public void Mining()
    {
        isChopping = !isChopping;
        animator.SetBool("isMining", !isChopping);
        //navAgent.enabled = false;
    }

    private void UpdateAnimator()
    {
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        GetComponent<Animator>().SetFloat("forwardSpeed", speed);
    }
}

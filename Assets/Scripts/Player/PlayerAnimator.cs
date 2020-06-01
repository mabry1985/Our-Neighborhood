using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimator : MonoBehaviour
{
    const float animSmoothTime = .1f;

    NavMeshAgent agent;
    Animator animator;

    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        float speedPercent =  agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speedPercent", speedPercent, animSmoothTime, Time.deltaTime);
    }

    public void FarmAnimHandler(Player player, string material) 
    {
        switch (material)
        {
            case "Stone":
            player.animator.SetBool("isMining", true);
            break;
            case "Wood":
            player.animator.SetBool("isChopping", true);
            break;
            case "Wheat":
            player.animator.SetBool("isGathering", true);
            break;
            case "Water":
            player.animator.SetBool("isGathering", true);
            break;
        }
    }
}

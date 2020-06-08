using UnityEngine;
using UnityEngine.AI;

public class DefaultBehaviour : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var navAgent = animator.GetComponent<NavMeshAgent>();
        
        if (navAgent.enabled == false)
            navAgent.enabled = true;

        if (navAgent.tag != "Player") return;
        
        animator.SetBool("isWaving", false);
        animator.SetBool("isStanding", true);

        animator.GetComponent<Player>().isStanding = true;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    
    }

    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
   
    }

    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      
    }
}
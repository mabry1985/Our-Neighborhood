using UnityEngine;

public class AnimController
{
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
}

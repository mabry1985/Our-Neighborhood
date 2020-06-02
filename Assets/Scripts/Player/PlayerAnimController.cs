using UnityEngine;

public class PlayerAnimController
{
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

    public void CancelAnimations(Player player)
    {
        //var anim = player.GetComponentInChildren<Animation>();
        player.animator.SetBool("isMining", false);
        //anim.Stop("isMining");

        player.animator.SetBool("isChopping", false);
        //anim.Stop("isChopping");
        
        player.animator.SetBool("isGathering", false);
        //anim.Stop("isGathering");
        
        player.animator.SetBool("isStanding", true);
    }
}

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
        player.animator.SetBool("isMining", false);

        player.animator.SetBool("isChopping", false);
        
        player.animator.SetBool("isGathering", false);
        
        player.animator.SetBool("isStanding", true);
    }
}

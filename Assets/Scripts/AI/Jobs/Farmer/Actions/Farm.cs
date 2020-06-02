using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : GAction
{

    public GInventory inv;
    public GAgent gAgent;
    public Player player;
    public Bot bot;

    public override bool PrePerform()
    {

        player = this.GetComponent<Player>();
        //bot = gameObject.transform.parent.parent.GetComponent<Bot>();
        gAgent = gameObject.GetComponent<GAgent>();

        if (gameObject.tag == "Player")
        {   
            inv = player.inventory;
            if (inv.invSpace == 0) 
            {
                player.playerAnimController.CancelAnimations(player);
                print("inventory is full");
                //player.ChangeJobs("Idle", null);
                beliefs.RemoveState("isFarming");
                return false;
            }
        }
        // else if (gameObject.transform.parent.parent.tag == "Bot")
        // {
        //     inv = bot.inventory;
        //     if (inv.invSpace == 0) {
        //         bot.ChangeJobs("Idle", null);
        //         return false;
        //     }
        // }
        return true;
    }

    public override bool PostPerform()
    {
        var material = gameObject.GetComponent<PlayerGAgent>().material;
        print($"{player.playerName} is farming " + material);
        
        if (gAgent.distanceToTarget > 2f)
            return false;

        AddToInventory(material);
        //player.playerAnimController.CancelAnimations(player);
        return true;
    }

    public void AddToInventory(string material)
    {
        if (this.tag == "Player")
        {
            inv = player.inventory;
            inv.invSpace -= 1;
        }
        // else if (gameObject.transform.parent.parent.tag == "Bot")
        // {
        //     inv = bot.inventory;
        //     inv.invSpace -= 1;
        // }

        if (inv.items.ContainsKey(material))
            inv.items[material] = inv.items[material] += 1;
        else
            inv.AddItem(material, 1);
    }
}

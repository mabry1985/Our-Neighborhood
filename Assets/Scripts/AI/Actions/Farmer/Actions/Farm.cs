using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : GAction
{

    public GInventory inv;
    public GAgent gAgent;
    public ChatPlayerController player;
    public Bot bot;

    public override bool PrePerform()
    {
        player = this.GetComponent<ChatPlayerController>();
        gAgent = gameObject.GetComponent<GAgent>();

        if (gameObject.tag == "Player")
        {   
            inv = player.inventory;
            if (inv.invSpace == 0) 
            {
                player.playerAnimController.CancelAnimations(player.GetComponent<Animator>());
                print("inventory is full");
                beliefs.RemoveState("isFarming");
                return false;
            }
        }

        return true;
    }

    public override bool PostPerform()
    {
        var material = gameObject.GetComponent<ChatPlayerGAgent>().material;
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

        if (inv.items.ContainsKey(material))
            inv.items[material] = inv.items[material] += 1;
        else
            inv.AddItem(material, 1);
    }
}

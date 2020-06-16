using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMaterials : GAction
{
    private GInventory inv;
    private ChatPlayerController player;
    private Bot bot;
    private List<KeyValuePair<string, int>> craftingMaterials = new List<KeyValuePair<string, int>>();
    private string craftingItem;
    private bool hasMaterial;
    private ChatPlayerGAgent playerGAgent;

    public override bool PrePerform()
    {
        playerGAgent = this.GetComponent<ChatPlayerGAgent>();
        craftingItem = playerGAgent.craftingItem;
        hasMaterial = false;
        player = this.GetComponent<ChatPlayerController>();
        bot = this.GetComponent<Bot>();

        craftingMaterials = CraftingRecipes.recipes[craftingItem];

        if (gameObject.tag == "Player")
            inv = player.inventory;
   
        foreach (KeyValuePair<string, int> item in craftingMaterials)
        {
            if (GWorld.worldInventory.items[item.Key] < item.Value)
            {
                return false;
            }
            //if you have ALL of this particular material
            if (inv.items.ContainsKey(item.Key) && inv.items[item.Key] >= item.Value)
                hasMaterial = true;
            else
                hasMaterial = false;
        }

        if (hasMaterial) {
            this.GetComponent<GAgent>().beliefs.ModifyState("hasMaterials", 1);
            return false;
        }

        return true;
    }

    public override bool PostPerform()
    {
        craftingMaterials = CraftingRecipes.recipes[craftingItem];
        if (this.tag == "Player" && inv.invSpace == 0)
        {
            //player.ChangeJobs("Idle", null);
            return false;      
        }

        foreach (KeyValuePair<string, int> item in craftingMaterials)
        {
            if (GWorld.worldInventory.items[item.Key] < item.Value)
            {
                return false;
            }
            //if you have ALL of this particular material
            if (inv.items.ContainsKey(item.Key) && inv.items[item.Key] >= item.Value)
                hasMaterial = true;
            else
                hasMaterial = false;

            if (!hasMaterial)
                AddToInventory(item.Key, item.Value);
        }

        this.GetComponent<GAgent>().beliefs.ModifyState("hasMaterials", 1);
        return true;
    }


    public bool AddToInventory(string material, int amount)
    {   
        if (this.tag == "Player")
        {
            if(inv.items.ContainsKey(material))
            {
                var newAmount = amount -= inv.items[material];
                if (inv.invSpace >= newAmount)
                {
                    inv.invSpace -= newAmount;
                    inv.items[material] = inv.items[material] += newAmount;
                    GWorld.worldInventory.items[material] -= newAmount;
                }
                else
                    return false;
            }
            else
            {
                if (inv.invSpace >= amount)
                {
                    inv.invSpace -= amount;
                    inv.AddItem(material, amount);
                    GWorld.worldInventory.items[material] -= amount;
                }
                else
                    return false;
            }

        }
        // else if (gameObject.transform.parent.parent.tag == "Bot")
        // {
        //     int newAmount = amount - inv.items[material];
        //     if (inv.invSpace >= newAmount)
        //         inv.invSpace -= newAmount;
        //     else
        //         return false;

        //     GWorld.worldInventory.items[material] -= newAmount;
        // }

        return true;
    }

}
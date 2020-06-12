using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToWorkshop : GAction
{
    private GInventory inv;
    private PlayerGAgent gAgent;
    private Player player;
    private Bot bot;
    private List<KeyValuePair<string, int>> craftingMaterials = new List<KeyValuePair<string, int>>();
    private string craftingItem;

    public override bool PrePerform()
    {
        player = this.GetComponent<Player>();
        //bot = gameObject.transform.parent.parent.GetComponent<Bot>();
        gAgent = this.GetComponent<PlayerGAgent>();
        craftingItem = gAgent.craftingItem;
        var hasMaterial = false;
        craftingMaterials = CraftingRecipes.recipes[craftingItem];
        
        if (gameObject.tag == "Player")
        {
            inv = player.inventory;
        }
        // else if (gameObject.transform.parent.parent.tag == "Bot")
        // {
        //     inv = bot.inventory;
        // }

        foreach (KeyValuePair<string, int> item in craftingMaterials)
        {
            //if you have ALL of this particular material
            if (inv.items.ContainsKey(item.Key)){
                if(inv.items[item.Key] >= item.Value)
                    hasMaterial = true;
            }    
            else
                return false;
        }

        if (hasMaterial)
        {
            return true;
        }
        else
            return false;
    }

    public override bool PostPerform()
    {
        print("in crafting postperform");
        foreach (KeyValuePair<string, int> item in craftingMaterials)
        {
            inv.items[item.Key] -= item.Value;
            inv.invSpace += item.Value;
            if (inv.items[item.Key] <= 0)
            {
                inv.items.Remove(item.Key);
            }
        }

        AddCraftedToInventory(craftingItem, 1);

        gAgent.beliefs.RemoveState("hasMaterials");
        gAgent.beliefs.RemoveState("isCrafting");

        return true;
    }

    public bool AddCraftedToInventory(string craftedItem, int amount)
    {
        if (gameObject.tag == "Player")
        {
            if (inv.items.ContainsKey(craftedItem) && inv.invSpace >= amount)
            {
                    inv.items[craftedItem] += amount;
                    inv.invSpace -= amount;
            }
            else if (inv.invSpace >= amount)
            {
                    inv.invSpace -= amount;
                    inv.AddItem(craftedItem, amount);
            }
            else
                return false;
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

        //player.ChangeJobs("Idle", null);
        return true;

    }

}
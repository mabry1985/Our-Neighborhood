using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToWorkshop : GAction
{
    private GInventory inv;
    private GAgent gAgent;
    private Player player;
    private Bot bot;
    private List<KeyValuePair<string, int>> craftingMaterials = new List<KeyValuePair<string, int>>();
    private string material;

    public override bool PrePerform()
    {
        player = gameObject.transform.parent.parent.GetComponent<Player>();
        bot = gameObject.transform.parent.parent.GetComponent<Bot>();
        gAgent = gameObject.GetComponent<GAgent>();
        var job = this.gameObject.GetComponent<Crafter>();
        material = job.material;
        var hasMaterial = false;
        craftingMaterials = CraftingRecipes.recipes[material];
        
        print("in preperfom with materia " + material);
        if (gameObject.transform.parent.parent.tag == "Player")
        {
            inv = player.inventory;
        }
        else if (gameObject.transform.parent.parent.tag == "Bot")
        {
            inv = bot.inventory;
        }

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
        foreach (KeyValuePair<string, int> item in craftingMaterials)
        {
            inv.items[item.Key] -= item.Value;
            inv.invSpace += item.Value;
            if (inv.items[item.Key] <= 0)
            {
                inv.items.Remove(item.Key);
            }
        }

        print("adding to inventory" + material);
        AddToInventory(material, 1);

        this.GetComponent<GAgent>().beliefs.ModifyState("hasMaterials", -1);
        return true;
    }

    public bool AddToInventory(string material, int amount)
    {
        if (gameObject.transform.parent.parent.tag == "Player")
        {
            if (inv.items.ContainsKey(material) && inv.invSpace >= amount)
            {
                    inv.items[material] += amount;
                    inv.invSpace -= amount;
            }
            else if (inv.invSpace >= amount)
            {
                    inv.invSpace -= amount;
                    inv.AddItem(material, amount);
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

        return true;

    }

}
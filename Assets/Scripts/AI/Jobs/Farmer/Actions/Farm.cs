using System.Collections;
using System.Collections.Generic;

public class Farm : GAction
{

    public GInventory inv;
    public Player player;
    public Bot bot;

    public override bool PrePerform()
    {

        player = gameObject.transform.parent.parent.GetComponent<Player>();
        bot = gameObject.transform.parent.parent.GetComponent<Bot>();

        if (gameObject.transform.parent.parent.tag == "Player")
        {   
            inv = player.inventory;
            if (player.inventorySize == 0) {
                player.ChangeJobs("Idle", null);
                return false;
            }
        }
        else if (gameObject.transform.parent.parent.tag == "Bot")
        {
            inv = bot.inventory;
            if (bot.inventorySize == 0) {
                bot.ChangeJobs("Idle", null);
                return false;
            }
        }
        
        return true;
    }

    public override bool PostPerform()
    {
        player = gameObject.transform.parent.parent.GetComponent<Player>();
        bot = gameObject.transform.parent.parent.GetComponent<Bot>();

        var material = gameObject.GetComponent<Farmer>().material;
            print($"{player.playerName} is farming " + material);
        
        if (gameObject.transform.parent.parent.tag == "Player")
        {
            inv = player.inventory;
            player.inventorySize -= 1;
        }
        else if (gameObject.transform.parent.parent.tag == "Bot")
        {
            inv = bot.inventory;
            bot.inventorySize -= 1;
        }

        if (inv.items.ContainsKey(material))
            inv.items[material] = inv.items[material] += 1;
        else 
            inv.AddItem(material, 1);

        return true;
    }
}

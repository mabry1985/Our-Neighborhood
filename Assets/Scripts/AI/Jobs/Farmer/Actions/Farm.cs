using System.Collections;
using System.Collections.Generic;

public class Farm : GAction
{

    public GInventory inv;
    public GAgent gAgent;
    public Player player;
    public Bot bot;

    public override bool PrePerform()
    {

        player = gameObject.transform.parent.parent.GetComponent<Player>();
        bot = gameObject.transform.parent.parent.GetComponent<Bot>();
        gAgent = gameObject.GetComponent<GAgent>();

        if (gameObject.transform.parent.parent.tag == "Player")
        {   
            inv = player.inventory;
            if (inv.invSpace == 0) {
                player.ChangeJobs("Idle", null);
                return false;
            }
        }
        else if (gameObject.transform.parent.parent.tag == "Bot")
        {
            inv = bot.inventory;
            if (inv.invSpace == 0) {
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
        
        if (gAgent.distanceToTarget > 2f)
            return false;

        AddToInventory(material);
        return true;
    }


    public void AddToInventory(string material)
    {
        if (gameObject.transform.parent.parent.tag == "Player")
        {
            inv = player.inventory;
            inv.invSpace -= 1;
        }
        else if (gameObject.transform.parent.parent.tag == "Bot")
        {
            inv = bot.inventory;
            inv.invSpace -= 1;
        }

        if (inv.items.ContainsKey(material))
            inv.items[material] = inv.items[material] += 1;
        else
            inv.AddItem(material, 1);
    }
}

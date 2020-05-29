using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GetWarm : GAction
{
    Player player;

    public override bool PrePerform()
    {
        player = transform.parent.GetComponentInParent<Player>();
        print("in get warm preperform");
        return true;
    }
    public override bool PostPerform()
    {
        player.SitDown();
        //agent.GetComponent<GAgent>().beliefs.SetState("isWarm", 1);
        return true;
    }
}

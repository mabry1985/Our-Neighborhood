using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseRestroom : GAction
{
    public override bool PrePerform()
    {
        target = GWorld.Instance.GetQueue("toilets").RemoveResource();
        Debug.Log("in restroom" + target);
        if (target == null)
            return false;
        inventory.AddItem(target);
        GWorld.Instance.GetWorld().ModifyState("FreeToilet", -1);
        Debug.Log("headed to restroom");
        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.GetQueue("toilets").AddResource(target);
        inventory.RemoveItem(target);
        GWorld.Instance.GetWorld().ModifyState("FreeToilet", 1);
        beliefs.RemoveState("bladderFull");
        Debug.Log("ahhhhh, much better");
        return true;
    }
}

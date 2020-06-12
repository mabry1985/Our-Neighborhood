using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanPuddle : GAction
{
GameObject resource;

    public override bool PrePerform()
    {
        target = GWorld.Instance.GetQueue("puddles").RemoveResource();
        print(target);
        if (target == null)
            return false;
            
        inventory.AddItem(target);
        GWorld.Instance.GetWorld().ModifyState("FreePuddle", -1);
        return true;
    }
    
    public override bool PostPerform()
    {

        inventory.RemoveItem(target);
        Destroy(target);
        beliefs.RemoveState("clean");

        return true;
    }
}

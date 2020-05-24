using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoHome : GAction
{
    public override bool PrePerform()
    {
        print("in gohome preperform");
        return true;
    }
    
    public override bool PostPerform()
    {
        this.transform.parent.GetComponentInParent<Player>().inventory.TransferToHomeInventory();
        print("Im home");
        return false;
    }
}

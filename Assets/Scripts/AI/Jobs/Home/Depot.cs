using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Depot : GAction
{
    public override bool PrePerform()
    {
        //Debug.Log("in go home preperform");
        return true;
    }
    
    public override bool PostPerform()
    {
        this.GetComponent<Player>().inventory.TransferToWorldInventory();
        beliefs.RemoveState("depot");
        //print("Im home");
        return true;
    }
}

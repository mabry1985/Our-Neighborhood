using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : GAction
{
    public override bool PrePerform()
    {
        return true;
    }
    public override bool PostPerform()
    {
        this.GetComponent<GAgent>().beliefs.ModifyState("inDanger", -1);
        return true;
    }
}

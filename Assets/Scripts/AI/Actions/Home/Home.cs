using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : GAgent
{
 
    new void Start()
    {
        base.Start();
        SubGoal s1 = new SubGoal("isHome", 1, false);
        goals.Add(s1, 3);
        SubGoal s3 = new SubGoal("isWarm", 1, false);
        goals.Add(s3, 4);
        SubGoal s4 = new SubGoal("hasFriend", 1, false);
        goals.Add(s4, 4);


    }

}

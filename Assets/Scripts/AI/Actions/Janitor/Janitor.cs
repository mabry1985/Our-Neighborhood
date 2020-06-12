using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Janitor : GAgent
{
    // Start is called before the first frame update
    new void Start()
    {
        // Call base Start method
        base.Start();
        // Set goal so that it can't be removed so the nurse can repeat this action
        SubGoal s1 = new SubGoal("clean", 1, false);
        goals.Add(s1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

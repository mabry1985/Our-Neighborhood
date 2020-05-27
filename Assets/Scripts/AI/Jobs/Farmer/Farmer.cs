using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class Farmer : GAgent
{
	public string material;

	private Farmer()
	{
		
	}

    private void OnEnable() {
    }

    new void Start()
    {
        base.Start();
        SubGoal s1 = new SubGoal("farm" + material, 1, false);
        goals.Add(s1, 3);
        SubGoal s2 = new SubGoal("isSafe", 1, false);
        goals.Add(s2, 5);
        
        //this.GetComponent<GAgent>().beliefs.ModifyState("notWorking", 1);
        //Invoke("GetTired", Random.Range(2.0f, 20.0f))
    }


    void GetTired()
    {

        beliefs.ModifyState("exhausted", 0);
        //call the get tired method over and over at random times to make the nurse
        //get tired again
        Invoke("GetTired", Random.Range(0.0f, 20.0f));
    }

}

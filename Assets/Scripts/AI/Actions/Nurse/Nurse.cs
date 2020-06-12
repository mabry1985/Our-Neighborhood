using UnityEngine;

public class Nurse : GAgent
{

    new void Start()
    {

        // Call base Start method
        base.Start();
        // Set goal so that it can't be removed so the nurse can repeat this action
        SubGoal s1 = new SubGoal("treatPatient", 1, false);
        goals.Add(s1, 3);

        // Resting goal
        SubGoal s2 = new SubGoal("rested", 1, false);
        goals.Add(s2, 1);

        SubGoal s3 = new SubGoal("bladderEmpty", 1, false);
        goals.Add(s3, 5);

        // Call the GetTired() method for the first time
        Invoke("GetTired", Random.Range(10.0f, 20.0f));
        Invoke("BladderFull", Random.Range(5.0f, 10.0f));

    }

    void GetTired()
    {

        beliefs.ModifyState("exhausted", 0);
        //call the get tired method over and over at random times to make the nurse
        //get tired again
        Invoke("GetTired", Random.Range(0.0f, 20.0f));
    }

    void BladderFull()
    {
        beliefs.ModifyState("bladderFull", 0);

        Invoke("BladderFull", Random.Range(5.0f, 10.0f));

    }

}
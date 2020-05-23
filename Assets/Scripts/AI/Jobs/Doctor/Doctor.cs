using UnityEngine;

public class Doctor : GAgent
{

    new void Start()
    {
        base.Start();

        SubGoal s1 = new SubGoal("rested", 1, false);
        goals.Add(s1, 3);

        SubGoal s2 = new SubGoal("research", 1, false);
        goals.Add(s2, 1);

        SubGoal s3 = new SubGoal("bladderEmpty", 1, false);
        goals.Add(s3, 5);

        Invoke("GetTired", Random.Range(5.0f, 10.0f));
        Invoke("BladderFull", Random.Range(3.0f, 15.0f));
    }

    void GetTired()
    {

        beliefs.ModifyState("exhausted", 0);

        Invoke("GetTired", Random.Range(3.0f, 10.0f));
    }

    void BladderFull()
    {
        beliefs.ModifyState("bladderFull", 0);

        Invoke("BladderFull", Random.Range(5.0f, 10.0f));

    }

}
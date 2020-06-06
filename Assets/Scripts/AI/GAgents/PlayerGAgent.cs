using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class PlayerGAgent : GAgent
{
    public string material = "";
    public string craftingItem = "";
    public float senseDistance = 50f;

    new void Start()
    {
        base.Start();
        SubGoal s1 = new SubGoal("farm" + material, 1, false);
        goals.Add(s1, 3);
        SubGoal s2 = new SubGoal("isSafe", 1, false);
        goals.Add(s2, 5);
        SubGoal s3 = new SubGoal("isWarm", 1, false);
        goals.Add(s3, 5);
        SubGoal s4 = new SubGoal("isLonely", 1, false);
        goals.Add(s4, 5);
        SubGoal s5 = new SubGoal("craftedItem", 1, false);
        goals.Add(s5, 3);
        SubGoal s6 = new SubGoal("depotInventory", 1, false);
        goals.Add(s6, 4);

        //Invoke("GetTired", Random.Range(2.0f, 20.0f))
    }

    private void Update() {
    }

    void GetTired()
    {
        beliefs.ModifyState("exhausted", 0);
        //call the get tired method over and over at random times to make the nurse
        //get tired again
        Invoke("GetTired", Random.Range(0.0f, 20.0f));
    }

    public void SetFear()
    {
        beliefs.ModifyState("inDanger", 0);
    }

    public void RemoveFear()
    {
        beliefs.RemoveState("inDanger");
    }

    IEnumerator CheckSurroundings(Vector3 center, float radius)
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            Collider[] hitColliders = Physics.OverlapSphere(center, radius);
            int i = 0;
            while (i < hitColliders.Length)
            {
                if(hitColliders[i].GetComponent<CombatTarget>())
                {
                    SetFear();
                }
                i++;
            }
            yield return new WaitForSeconds(5);
            RemoveFear();
        }
    }
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class PlayerGAgent : GAgent
{
    public string material = "";
    public string craftingItem = "";
    public float senseDistance = 100f;
    CapsuleCollider senseTrigger;
    Player player;

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

        player = GetComponent<Player>();
    }

    float lastUpdate;
    float updateInterval = 2.0f;

    new void LateUpdate()
    {
        base.LateUpdate();

        if (Time.time > lastUpdate + updateInterval)
        {
            CheckSurroundings();
            lastUpdate = Time.time;
        }
    }

    public void SetFear()
    {
        beliefs.ModifyState("inDanger", 1);
    }

    public void RemoveFear()
    {
        beliefs.RemoveState("inDanger");
    }

    void CheckSurroundings()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, senseDistance);
        int i = 0;
        while (i < hitColliders.Length)
        {
            CombatTarget target = hitColliders[i].transform.root.GetComponent<CombatTarget>();
            print(hitColliders[i].transform.root.name);

            if (target != null)
            {
                print("Im scared");
                player.InDanger();
                break;
            }
            i++;
        }
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, senseDistance);    
    }

}
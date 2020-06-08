using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGAgent : GAgent
{
    public float senseDistance = 100f;
    CapsuleCollider senseTrigger;
    List<GameObject> players = new List<GameObject>();
    ProjectileSpawnPoint projectileSpawnPoint;

    new void Start()
    {
        base.Start();
        SubGoal s1 = new SubGoal("attackPlayer", 1, false);
        goals.Add(s1, 4);
        SubGoal s2 = new SubGoal("isSafe", 1, false);
        goals.Add(s2, 5);

        if (GetComponentInChildren<ProjectileSpawnPoint>() != null)
        {
            beliefs.ModifyState("hasProjectile", 1);
        }

        beliefs.ModifyState("canAttack", 1);
    }

    private void Update() 
    {
        base.Update();
        print(timeSinceLastAttack + " / " + timeBetweenAttack);
        if (timeSinceLastAttack >= timeBetweenAttack)
        {
            beliefs.ModifyState("canAttack", 1);
        }
    }

    float lastUpdate;
    float updateInterval = .5f;

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
        int playerCount = 0;
        while (i < hitColliders.Length)
        {
            Player target = hitColliders[i].transform.root.GetComponent<Player>();
            if (target != null)
            {
                if(DeathCheck(target)) break;
            }
            
            if (target != null && !players.Contains(target.gameObject))
            {
                players.Add(target.gameObject);
                beliefs.ModifyState("playerNear", 1);
                playerCount++;
            }
            i++;
        }

        //print(playerCount);
        if (players.Count == 0) 
        {
            beliefs.RemoveState("playerNear");
            return;
        }

        SetTarget();
    }

    private bool DeathCheck(Player target)
    {
        if (target.GetComponent<Health>().IsDead())
        {
            players.Remove(target.gameObject);
            beliefs.ModifyState("playerNear", -1);
            return true;
        }
        return false;
    }      

    private void SetTarget()
    {
        GameObject target = GetComponent<EnemyController>().GetClosestEnemy(players).gameObject;
        GetComponent<Attack>().target = target;
        GetComponent<ProjectileAttack>().target = target;
        GetComponent<Chase>().target = target;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, senseDistance);
    }
}

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
        SubGoal s3 = new SubGoal("lookingForPlayer", 1, false);
        goals.Add(s3, 3);

        if (GetComponentInChildren<ProjectileSpawnPoint>() != null)
        {
            beliefs.ModifyState("hasProjectile", 1);
        }
    }

    new private void Update()
    {
        base.Update();

        if (timeSinceLastAttack >= timeBetweenAttack)
        {
            beliefs.ModifyState("canAttack", 1);
        }

        IdleCheck();

    }

    float lastUpdate;
    float updateInterval = .2f;

    new void LateUpdate()
    {
        base.LateUpdate();

        if (Time.time > lastUpdate + updateInterval)
        {
            CheckSurroundings();
            lastUpdate = Time.time;
        }
    }

    private void IdleCheck()
    {
        if (players.Count > 0)
        {
            beliefs.RemoveState("idle");
        }
        else
        {
            beliefs.ModifyState("idle", 1);
        }
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
                if(DeathCheck(target))
                {
                    players.Remove(target.gameObject);
                    break;
                }
            }
            
            if (target != null)
            {

                players.Add(target.gameObject);
                beliefs.ModifyState("playerNear", 1);
                playerCount++;
            }
            i++;
        }
        //print(playerCount);
        if (playerCount == 0) 
        {
            beliefs.RemoveState("playerNear");
            beliefs.ModifyState("idle", 1);
            beliefs.RemoveState("inRange");
            players.Clear();
            RemoveAttackTarget();
            return;
        }

        SetAttackTarget();
    }

    private bool DeathCheck(Player target)
    {
        if (target.GetComponent<Health>().IsDead())
            return true;
        else 
            return false;
    }      

    private void SetAttackTarget()
    {
        GameObject target = GetComponent<EnemyController>().GetClosestEnemy(players).gameObject;
        GetComponent<Attack>().target = target;
        GetComponent<ProjectileAttack>().target = target;
        GetComponent<Chase>().target = target;
    }

    private void RemoveAttackTarget()
    {
        GetComponent<Chase>().target = null;
        GetComponent<Attack>().target = null;
        GetComponent<ProjectileAttack>().target = null;
    }

    // public void SetFear()
    // {
    //     beliefs.ModifyState("inDanger", 1);
    // }

    // public void RemoveFear()
    // {
    //     beliefs.RemoveState("inDanger");
    // }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, senseDistance);
    }
}

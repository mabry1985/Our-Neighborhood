using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGAgent : GAgent
{
    public float senseDistance = 100f;
    CapsuleCollider senseTrigger;
    List<GameObject> players = new List<GameObject>();
    List<GameObject> friendsAndFoes = new List<GameObject>();
    ProjectileSpawnPoint projectileSpawnPoint;
    int playerCount = 0;
    [SerializeField] float tooCloseDistance = 2f;

    new void Start()
    {
        base.Start();
        SubGoal s1 = new SubGoal("attackPlayer", 2, false);
        goals.Add(s1, 2);
        SubGoal s2 = new SubGoal("isSafe", 1, false);
        goals.Add(s2, 1);
        SubGoal s3 = new SubGoal("lookingForPlayer", 4, false);
        goals.Add(s3, 4);
        // SubGoal s4 = new SubGoal("keepSpace", 3, false);
        // goals.Add(s4, 3);

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

        if (closestTargetDistance < tooCloseDistance)
        {
            beliefs.ModifyState("tooClose", 1);
        }
        else
        {
            beliefs.RemoveState("tooClose");
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
            SetClosestDistance(friendsAndFoes);

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
        playerCount = 0;
        while (i < hitColliders.Length)
        {
            CheckForPlayers(hitColliders, i);
            FriendsAndFoes(hitColliders, i);
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
    
    void CheckForPlayers(Collider[] hitColliders, int i)
    {
        Player target = hitColliders[i].transform.root.GetComponent<Player>();
        if (target != null)
        {
            if (DeathCheck(target))
            {
                players.Remove(target.gameObject);
                return;
            }
        }

        if (target != null)
        {
            players.Add(target.gameObject);
            beliefs.ModifyState("playerNear", 1);
            playerCount++;
        }
    }

    void FriendsAndFoes(Collider[] hitColliders, int i)
    {
        Health target = hitColliders[i].transform.root.GetComponent<Health>();
        if (target != null)
        {
            if (target.IsDead())
            {
                friendsAndFoes.Remove(target.gameObject);
                return;
            }
        }

        if (target != null && target.GetComponent<EnemyGAgent>() != this)
        {
            
            friendsAndFoes.Add(target.gameObject);
        }
    }

    private bool DeathCheck(Player target)
    {
        return target.GetComponent<Health>().IsDead();
    }      

    private void SetAttackTarget()
    {
        GameObject target = GetClosestTarget(players).gameObject;
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

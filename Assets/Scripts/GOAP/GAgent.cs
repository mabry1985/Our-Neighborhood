using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SubGoal 
{
    public Dictionary<string, int> sGoals;
    public bool remove;

    public SubGoal(string s, int i, bool r)
    {
        sGoals = new Dictionary<string, int>();
        sGoals.Add(s, i);
        remove = r;
    }
}

public class GAgent : MonoBehaviour
{
    public List<GAction> actions = new List<GAction>();
    public Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();
    public WorldStates beliefs = new WorldStates();

    public GPlanner planner;
    public Queue<GAction> actionQueue;
    public GAction currentAction;
    SubGoal currentGoal;
    public Vector3 destination = Vector3.zero;
    public float distanceToTarget;
    public bool invoked = false;

    public float timeSinceLastAttack;
    public float timeBetweenAttack = 10f;

    //set in get closest enemy function
    public float closestTargetDistance;

    protected void Start()
    {
        GAction[] acts = this.GetComponents<GAction>();
        foreach(GAction a in acts) 
        {
            actions.Add(a);
        }
    }

    protected void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
    }

    void CompleteAction()
    {
        currentAction.running = false;
        currentAction.PostPerform();
        invoked = false;
    }

    protected void LateUpdate()
    {
        if (currentAction != null && currentAction.running)
        {
            distanceToTarget = Vector3.Distance(destination, this.transform.position);
            //Debug.Log(currentAction.navMeshAgent.hasPath + "   " + distanceToTarget);
            if (distanceToTarget < currentAction.range)
            {
                //Debug.Log("Distance to Goal: " + currentAction.navMeshAgent.remainingDistance);
                if (!invoked)
                {
                    // Placed HandlePlayer here instead of PlayerGAgent because not sure how to call it from a class that uses this as a base
                    HandlePlayer();
                    Invoke("CompleteAction", currentAction.duration);
                    invoked = true;
                }
            }
            return;
        }

        if (planner == null || actionQueue == null)
        {
            planner = new GPlanner();

            var sortedGoals = from entry in goals orderby entry.Value descending select entry;

            foreach(KeyValuePair<SubGoal, int> sg in sortedGoals)
            {
                actionQueue = planner.plan(actions, sg.Key.sGoals, beliefs);
                if(actionQueue != null)
                { 
                    currentGoal = sg.Key;
                    break;
                }
            }
        }

        if (actionQueue != null && actionQueue.Count == 0)
        {
            if (currentGoal.remove)
            {
                goals.Remove(currentGoal);
            }

            planner = null;
        }        

        if (actionQueue != null && actionQueue.Count > 0)
        {
            currentAction = actionQueue.Dequeue();
            if (currentAction.PrePerform())
            {
                if (currentAction.target == null && currentAction.targetTag != "")
                    currentAction.target = GameObject.FindWithTag(currentAction.targetTag);

                //print(currentAction.target + " is the current action target");
                if (currentAction.target != null)
                {
                    currentAction.running = true;

                    destination = currentAction.target.transform.position;
                    Transform dest = currentAction.target.transform.Find("Destination");

                    if (dest != null)
                        destination = dest.position;

                    if (!GetComponent<Health>().IsDead())
                        currentAction.navMeshAgent.SetDestination(destination);
                }
            }
            else
            {
                actionQueue = null;
            }
        }
    }

    private void HandlePlayer()
    {
        ChatPlayerController player = GetComponent<ChatPlayerController>();

        if (player != null)
        {
            player.progressBar.gameObject.SetActive(true);
            StartCoroutine(player.progressBar.GetComponent<ActionProgressBar>().IncrementProgress(currentAction.duration));

            if (currentAction.actionName == "Farm")
            {
                player.playerAnimController.FarmAnimHandler(player.animator, currentAction.targetTag);
            }
        }
    }

    public Transform GetClosestTarget(List<GameObject> targets)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (GameObject potentialTarget in targets)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;

            if (dSqrToTarget < closestDistanceSqr)
            {
                closestTargetDistance = dSqrToTarget;
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget.transform;
            }
        }

        return bestTarget;
    }

    public void SetClosestDistance(List<GameObject> friendsAndFoes)
    {
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (GameObject potentialTarget in friendsAndFoes)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;

            if (dSqrToTarget < closestDistanceSqr)
            {
                closestTargetDistance = dSqrToTarget;
            }
        }

    }

    public void CancelGoap()
    {
        GameObject currentActionTarget;

        if (currentAction != null)
        {
            currentActionTarget = currentAction.target;
            currentAction.target = null;
            //playerGAgent.planner = null;
            invoked = false;
        }

        if (actionQueue != null)
        {
            actionQueue.Clear();
            currentAction.running = false;
        }
    }

    public void SetHasPlan()
    {
        beliefs.ModifyState("idle" ,1);
    }
}

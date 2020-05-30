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
    public PlayerAnimator playerAnimator = new PlayerAnimator();

    GPlanner planner;
    public Queue<GAction> actionQueue;
    public GAction currentAction;
    SubGoal currentGoal;
    Vector3 destination = Vector3.zero;

    Player player;
    public float distanceToTarget;

    public void Start()
    {
        GAction[] acts = this.GetComponents<GAction>();
        foreach(GAction a in acts) {
            actions.Add(a);
        }

        player = this.transform.parent.parent.gameObject.GetComponent<Player>();
        playerAnimator = player.GetComponent<PlayerAnimator>();
    }

    bool invoked = false;
    void CompleteAction()
    {
        currentAction.running = false;
        currentAction.PostPerform();
        invoked = false;
    }

    void LateUpdate()
    {
        if (currentAction != null && currentAction.running)
        {
            distanceToTarget = Vector3.Distance(destination, this.transform.position);
           //Debug.Log(currentAction.agent.hasPath + "   " + distanceToTarget);
            if (distanceToTarget < 2f)
            {

                // Debug.Log("Distance to Goal: " + currentAction.agent.remainingDistance);
                if (!invoked)
                {

                    print(currentAction.actionName);
                    print(currentAction.targetTag);

                    var entry = currentGoal.sGoals.First();
                    var first = currentGoal.sGoals.First();
                    string key = first.Key;
                    if(currentAction.actionName == "Farm")
                    {
                        //playerAnimator.FarmAnimHandler(player, currentAction.targetTag);
                    }
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

        if(actionQueue != null && actionQueue.Count == 0)
        {
            if(currentGoal.remove)
            {
                goals.Remove(currentGoal);
            }

            planner = null;
        }        

        if(actionQueue != null && actionQueue.Count > 0)
        {
            currentAction = actionQueue.Dequeue();
            if(currentAction.PrePerform())
            {
                if(currentAction.target == null && currentAction.targetTag != "")
                    currentAction.target = GameObject.FindWithTag(currentAction.targetTag);

                //print(currentAction.target + " is the current action target");
                if(currentAction.target != null)
                {
                    currentAction.running = true;

                    destination = currentAction.target.transform.position;
                    Transform dest = currentAction.target.transform.Find("Destination");

                    if(dest != null)
                        destination = dest.position;

                    if (!player.isDead)
                        currentAction.agent.SetDestination(destination);
                }
            }
            else
            {
                actionQueue = null;
            }
        }
    }
}

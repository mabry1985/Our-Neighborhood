using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class Player : MonoBehaviour
{

    public List<GameObject> jobs;
    public NavMeshAgent agent;
    public Canvas playerNameCanvas;
    public string playerName;
    public Text playerNameText;
    public static Player instance;

    public PlayerManager playerManager;
    public GInventory inventory;
    public int inventorySize = 5;

    public GameObject idle;
    //public GameObject baker;
    public GameObject farmer;
    
    public int playerID;

    private enum Job
    {
        Idle,
       // Baker,
        Farmer
    }

    private Job job;

    private void Start() {
        playerManager.playerReferences[playerID] = this;
        inventory = new GInventory();
    }

    private void Awake() 
    {
        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        if (instance == null)
        {
            instance = this;
        }

        var playerRenderer = gameObject.GetComponent<Renderer>();
        playerRenderer.material.SetColor("_Color", Color.black);
        
        ChangeJobs("Idle", null);
    }

    void LateUpdate()
    {
        playerNameCanvas.transform.rotation = Camera.main.transform.rotation;
    }

    public void JobSwitch(List<string> arg)
    {
        var jobEnum = Enum.Parse(typeof(Job), arg[0]);
        var job = arg[0];
        switch (jobEnum)
        {
            case Job.Idle:
                ChangeJobs(job, null);
                break;
            // case Job.Baker:
            //     ChangeJobs(arg);
            //     break;
            case Job.Farmer:
                var material = arg[1];
                ChangeJobs(job, material);
                break;
            default:
                break;
        }
    }

    public void ChangeJobs(string job, string material)
    {
    
        var jobCount = Enum.GetNames(typeof(Job)).Length;

        for (int i = 0; i < jobCount; i++)
        {
            if (jobs[i].tag == job)
            {
                if (job == "Farmer") {
                    jobs[i].GetComponent<Farmer>().material = material;
                    jobs[i].GetComponent<Farm>().targetTag = material;
                    jobs[i].GetComponent<Farm>().afterEffects[0].key = "farm" + material;
                }

                jobs[i].SetActive(true);
            }
            else if (jobs[i].tag != job)
            {
                jobs[i].SetActive(false);
            }
            else if (jobs[i].tag == "Untagged")
            {
                Debug.Log(jobs[i] + "needs to be tagged with the enum job value");
            }

        }
    }

}
    
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class Bot : MonoBehaviour
{

    public List<GameObject> jobs;
    public NavMeshAgent agent;
    public Canvas botNameCanvas;
    public string botName;
    public string currentJob;
    public int botID;
    public Text botNameText;
    public static Bot instance;

    public BotManager botManager;

    public GInventory inventory = new GInventory();
    public int inventorySize = 5;

    private enum Job
    {
        Idle,
        GoHome,
       // Baker,
        Farmer
    }

    private Job job;

    private void Start()
    {
        //inventory.name = this.botName;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        var playerRenderer = gameObject.GetComponent<Renderer>();
        playerRenderer.material.SetColor("_Color", Color.black);
        
        JobSwitch("Idle", null);
    }

    void LateUpdate()
    {
        botNameCanvas.transform.rotation = Camera.main.transform.rotation;
    }

    public void JobSwitch(string job, string material)
    {
        var jobEnum = Enum.Parse(typeof(Job), job);

        switch (jobEnum)
        {
            case Job.Idle:
                ChangeJobs(job, material);
                break;
            case Job.GoHome:
                ChangeJobs(job, material);
                break;
            // case Job.Baker:
            //     ChangeJobs(job, material);
            //     break;
            case Job.Farmer:
                ChangeJobs(job, material);
                break;
            default:
                break;
        }
    }

    public void ChangeJobs(string job, string material)
    {
        this.currentJob = job;

        var jobCount = Enum.GetNames(typeof(Job)).Length;

        for (int i = 0; i < jobCount; i++)
        {
            if (jobs[i].tag == job)
            {
                if (job == "Farmer")
                {
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

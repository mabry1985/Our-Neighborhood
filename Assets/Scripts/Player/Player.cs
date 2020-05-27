using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    public List<GameObject> jobs;
    public NavMeshAgent agent;
    public GInventory inventory;
    public PlayerManager playerManager;
    public PlayerController playerController;
    public Animator animator;
    public Transform home;

    public static Player instance;
   
    public int playerID;
    public string playerName;
    public Canvas playerNameCanvas;
    public Text playerNameText;
    public bool isDead = false;
    
    public string currentJob;
    public int inventorySize = 5;

    private enum Job
    {
        Idle,
        GoHome,
       // Baker,
        Farmer
    }

    private Job job;

    private void Start() {
        animator = this.GetComponentInChildren<Animator>();
        playerController = this.GetComponent<PlayerController>();
        home = playerManager.spawnPoint;


        playerManager.playerReferences[playerID] = this;
        inventory = new GInventory();
        inventory.invSpace = inventorySize;
        inventory.player = this;
    }

    private void Awake() 
    {
        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        if (instance == null)
        {
            instance = this;
        }

        var playerRenderer = gameObject.GetComponentInChildren<Renderer>();
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
            case Job.GoHome:
                ChangeJobs(job, null);
                break;
            // case Job.Baker:
            //     ChangeJobs(arg);
            //     break;
            case Job.Farmer:
                var material = arg[1];
                print(material);
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
            jobs[i].SetActive(false);
            if (jobs[i].tag == job)
            {
                if (job == "Farmer") {
                   HandleFarmer(material, jobs[i]);
                }
                jobs[i].SetActive(true);
            }
        }
    }

    private void HandleFarmer(string material, GameObject job)
    {

        var farm = job.GetComponent<Farm>();

        var d = GameObject.FindGameObjectWithTag(material);
        job.GetComponent<Farmer>().material = material;
        farm.GetComponent<Farm>().targetTag = material;
        farm.GetComponent<Farm>().target = d;
        farm.GetComponent<Farm>().afterEffects[0].key = "farm" + material;
    }

    public void OnDeath() 
    {
        if (this.tag == "Streamer")
        {
            var cam = playerController.vcam;
            //cam.m_Follow = null;       
        }

        this.transform.GetChild(0).gameObject.SetActive(false);
        isDead = true;
        agent.enabled = false;
        animator.enabled = false;

        Invoke("OnRevive", 5);
    }

    public void OnRevive() 
    {
        this.transform.position = home.position;
        if (this.tag == "Streamer")
        {
            var cam = playerController.vcam;
           // cam.m_Follow = this.transform;
        }

        this.transform.GetChild(0).gameObject.SetActive(true);
        isDead = false;
        agent.enabled = true;
        animator.enabled = true;

        this.ChangeJobs("Idle", null);
        //ChangeJobs("Idle", null);
    }
}
    
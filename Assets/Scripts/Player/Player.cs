﻿using System;
using System.Collections;
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
    public bool isWorking = false;
    
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
        var gAgents = transform.GetChild(0).GetComponentsInChildren<GAgent>(true);

        foreach (var gAgent in gAgents)
        {
            var gameObject = gAgent.gameObject;
            if (gameObject.activeSelf)
            {
                CancelGoap(gAgent.transform);
                gameObject.SetActive(false);
            }

            if (gameObject.tag == job)
            {
                if (job == "Farmer")
                {
                    HandleFarmer(material, gameObject);
                }
                gameObject.SetActive(true);
            }
        }
    }

    private void HandleFarmer(string material, GameObject job)
    {
        var farm = job.GetComponent<Farm>();

        var d = GameObject.FindGameObjectWithTag(material) ?? null;

        if (d != null){
            job.GetComponent<Farmer>().material = material;
            farm.targetTag = material;
            farm.target = d; 
            farm.afterEffects[0].key = "farm" + material;
        }
    }

    public IEnumerator OnDeath() 
    {
        isDead = true;
        Transform job = gameObject.transform.GetChild(0);

        if (this.tag == "Streamer")
        {
            var cam = playerController.vcam;
            //cam.m_Follow = null;       
        }

        for (int i = 0; i < job.childCount; i++)
        {
            if (job.GetChild(i).gameObject.activeSelf == true)
            {
                var gAgent = job.GetChild(i);
                CancelGoap(gAgent);
                gAgent.gameObject.SetActive(false);
                //ChangeJobs("Idle", null);
            }
        }

        yield return new WaitForSeconds(0.3f);

        agent.enabled = false;
        animator.enabled = false;

        Invoke("OnRevive", 5);
    }

    public void OnRevive() 
    {
        isDead = false;
        agent.enabled = true;
        agent.isStopped = false;

        agent.Warp(home.position);
        
        if (this.tag == "Streamer")
        {
            var cam = playerController.vcam;
           // cam.m_Follow = this.transform;
        }

        animator.enabled = true;
    }

    public void CancelGoap(Transform gAgent)
    {
        var actionQueue = gAgent.GetComponent<GAgent>().actionQueue ?? null;
        if (actionQueue != null)
        {
            actionQueue.Clear();
            gAgent.GetComponent<GAgent>().currentAction.running = false;
        }    
    }
}
    
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
    public Inventory inventory;

    public GameObject idle;
    public GameObject baker;
    public GameObject farmer;
    
    private PlayerModel playerModel = new PlayerModel();
    public int playerID;

    private enum Job
    {
        Idle,
        Baker,
        Farmer
    }

    private Job job;

    private void Start() {
        playerManager.playerReferences[playerID] = this;
        inventory.name = this.playerName;
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
        JobSwitch("Idle");
    }

    void LateUpdate()
    {
        playerNameCanvas.transform.rotation = Camera.main.transform.rotation;
    }

    public void JobSwitch(string job)
    {
        var jobEnum = Enum.Parse(typeof(Job), job);

        switch (jobEnum)
        {
            case Job.Idle:
                ChangeJobs(job);
                break;
            case Job.Baker:
                ChangeJobs(job);
                break;
            case Job.Farmer:
                ChangeJobs(job);
                break;
            default:
                break;
        }
    }

    private void ChangeJobs(string job)
    {
        var jobCount = Enum.GetNames(typeof(Job)).Length;

        for (int i = 0; i < jobCount; i++)
        {
            if (jobs[i].tag == job)
            {
                jobs[i].SetActive(true);
                print(jobs[i] + "set active to true!");
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
    
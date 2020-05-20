﻿using PathCreation;
using UnityEngine;
using System.Collections;

public class Train : MonoBehaviour
{
    public PathCreator pathCreator;
    public PlayerManager playerManager; 
    public BotManager botManager;

    public float speed = 25;
    public bool isMoving = false;
    public bool enteringStation = false;
    public bool leavingStation = false;

    private float distanceTravelled;
    private float originalSpeed;
    
    void Start()
    {
        botManager = GameObject.Find("BotManager").GetComponent<BotManager>();
        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();

        originalSpeed = speed;
    }

    void Update()
    {
        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);

        //send this info to cars behind to see if it'll make them match the rotation when going around curves?
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);        
        
        if (enteringStation == true) 
        {
            speed = Mathf.Lerp(speed, 0, Time.deltaTime);
        }

        if(leavingStation == true) 
        {
            speed = Mathf.Lerp(speed, originalSpeed, Time.deltaTime);
        }

        if (speed < 0.3f) {
            StartTrain();
        }

    }

    public void StopTrain() 
    {
        this.enteringStation = true;
        this.leavingStation = false;
    }

    public void StartTrain() 
    {
        this.enteringStation = false;
        this.leavingStation = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.gameObject.tag){
            case "Enter Station Trigger":
                StopTrain();
                break;
            case "Spawn Trigger":
                print("in spawn trigger case");
                StartCoroutine(this.playerManager.CheckForPlayerSpawn());
                StartCoroutine(this.botManager.CheckForBotSpawn());
                break;
            default:
                break;
        }
    }
}

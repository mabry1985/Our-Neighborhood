using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    public PlayerManager playerManager;
    public PlayerController playerController;
    public PlaceableItemManager placeableItemManager;
    public Renderer playerRenderer;

    public NavMeshAgent navAgent;
    public PlayerGAgent playerGAgent;
    public GInventory inventory;
    public Animator animator;
    public Transform home;
    public GAction farm;
    //public GAction craft;
    public GAction depot;

    public GameObject questionMark;
    public GameObject itemSpawnPoint;

    public static Player instance;
   
    public int playerID;
    public string playerName;
    public int inventorySize = 20;

    public Canvas playerNameCanvas;
    public Text playerNameText;

    public bool isDead = false;
    public bool isWorking = false;
    public bool isStanding = true;
    public bool isFollowing = false;
    public bool isChopping = false;

    private Vector3 destination;
    private float distanceToTarget;
    

    private void Start() {
        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        placeableItemManager = GameObject.Find("PlaceableItemManager").GetComponent<PlaceableItemManager>();
        home = playerManager.spawnPoint;
        depot.targetTag = "Home";
        playerManager.playerReferences[playerID] = this;
        inventory = new GInventory() {};
        inventory.invSpace = inventorySize;
        inventory.player = this;
        playerRenderer = gameObject.GetComponentInChildren<Renderer>();
        
        if(this.tag == "Streamer")
        {
            playerController = this.GetComponent<PlayerController>();
        }
    }

    private void Awake() 
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void LateUpdate()
    {
        playerNameCanvas.transform.rotation = Camera.main.transform.rotation;
        if(isFollowing)
        {
            destination = GameObject.Find("Streamer").transform.position;
            distanceToTarget = Vector3.Distance(destination, transform.position);
            navAgent.SetDestination(destination);
        }

            if(distanceToTarget <= 2f)
                isFollowing = false;
    }

    public void SitDown()
    {
        if (isStanding)
        {
            //animator.SetFloat("speedPercent", 0.0f);
            animator.SetBool("isSittingGround", true);
            animator.SetBool("isStanding", false);
            navAgent.enabled = false;
            isStanding = false;
        }
        else
        {
            animator.SetBool("isSittingGround", false);
            animator.SetBool("isStanding", true);
            isStanding = true;
        }
    }

    public void WaveHello()
    {
        animator.SetBool("isWaving", true);
        navAgent.enabled = false;
    }

    public void Mining()
    {
        isChopping = !isChopping;
        animator.SetBool("isMining", !isChopping);
        //navAgent.enabled = false;
    }

    // public void FindFriend()
    // {
    //     this.GetComponent<GAgent>().beliefs.ModifyState("isLonely", 1);
    // }

    public void HandleFarming(string material)
    {
        CancelFarming();
        playerGAgent.beliefs.ModifyState("isFarming", 1);
        var d = GameObject.FindGameObjectWithTag(material) ?? null;

        if (d != null){
            playerGAgent.material = material;
            farm.targetTag = material;
            farm.target = d; 
            //farm.afterEffects[0].key = "farm" + material;
        }
    }

    public void HandleCrafting(string craftingItem)
    {
        CancelFarming();
        playerGAgent.beliefs.ModifyState("isCrafting", 1);
        var getMaterials = this.GetComponent<GetMaterials>();
        var goToWorkshop = this.GetComponent<GoToWorkshop>();
        this.GetComponent<PlayerGAgent>().craftingItem = craftingItem;  
    }

    public void PlaceItem(string i)
    {
        PlaceableItem placeableItem = new PlaceableItem();
        if (inventory.items.ContainsKey(i))
        {
            inventory.items[i] -= 1;
            inventory.invSpace += 1;

            if(inventory.items[i] == 0)
            {
                inventory.items.Remove(i);
            }
            if(placeableItemManager.placeableItems.ContainsKey(i))
            {
                placeableItem = placeableItemManager.placeableItems[i];
                print(placeableItem.DecayTime);
                ItemSpawn(placeableItem.Prefab, placeableItem.DecayTime);
            }
        }
        else
        {
            questionMark.SetActive(true);
        }
    }

    public void ItemSpawn(GameObject item, int decayTime){
        var i = Instantiate(item, itemSpawnPoint.transform.position, transform.rotation);
        Destroy(i, decayTime);
    }

    public void FaceTarget(Vector3 destination)
    {
        Vector3 lookPos = destination - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
    }

    public void OnDeath() 
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
                CancelGoap();
                gAgent.gameObject.SetActive(false);
                //ChangeJobs("Idle", null);
            }
        }

        navAgent.enabled = false;
        animator.enabled = false;

        Invoke("OnRevive", 5);
    }

    public void OnRevive() 
    {
        isDead = false;
        navAgent.enabled = true;
        navAgent.isStopped = false;

        navAgent.transform.position = home.position;
        
        if (this.tag == "Streamer")
        {
            var cam = playerController.vcam;
           // cam.m_Follow = this.transform;
        }

        animator.enabled = true;
    }

    public void CancelGoap()
    {   
        var currentAction = playerGAgent.currentAction ?? null;
        GameObject currentActionTarget;

        if (currentAction != null)
        {
            currentActionTarget = currentAction.target;
            playerGAgent.currentAction.target = null;
            //playerGAgent.planner = null;
            playerGAgent.invoked = false;
        }
        
        var actionQueue = playerGAgent.actionQueue ?? null;
        if (actionQueue != null)
        {
            playerGAgent.actionQueue.Clear();
            playerGAgent.currentAction.running = false;
        }

    }

    public void CancelFarming()
    {
        playerGAgent.material = "";
        farm.targetTag = "";
        farm.target = null;
        playerGAgent.beliefs.RemoveState("isFarming");

        CancelGoap();
    }
}
    
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
    public AnimController playerAnimController = new AnimController();

    public NavMeshAgent navAgent;
    public Animator animator;
    public PlayerGAgent playerGAgent;
    public GInventory inventory;
    public GAction farm;
    public GAction depot;
    public Slider progressBar;
    public Transform home;

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
    
    private void Start() 
    {
        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        placeableItemManager = GameObject.Find("PlaceableItemManager").GetComponent<PlaceableItemManager>();
        depot = GetComponent<Depot>(); 
        farm = GetComponent<Farm>();
        animator = GetComponent<Animator>();
        playerGAgent = GetComponent<PlayerGAgent>();
        depot.targetTag = "Home";
        home = playerManager.spawnPoint;
        playerManager.playerReferences[playerID] = this;
        inventory = new GInventory() {};
        inventory.invSpace = inventorySize;
        inventory.player = this;
        playerRenderer = gameObject.GetComponentInChildren<Renderer>();
        playerController = this.GetComponent<PlayerController>();
        
    }

    private void Awake() 
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update() 
    {
        UpdateAnimator();    
    }

    void LateUpdate()
    {
        if (playerNameCanvas != null)
            playerNameCanvas.transform.rotation = Camera.main.transform.rotation;
        
        if(isFollowing)
        {
            destination = GameObject.Find("Streamer").transform.position;
            distanceToTarget = Vector3.Distance(destination, transform.position);

            if(navAgent.enabled == true)
                navAgent.SetDestination(destination);
            
            if(distanceToTarget <= 2f)
                isFollowing = false;
        }
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

    public void CancelFarming()
    {
        playerGAgent.material = "";
        //print(farm.targetTag);
        farm.targetTag = "";
        farm.target = null;
        playerGAgent.beliefs.RemoveState("isFarming");

        progressBar.gameObject.SetActive(false);
        playerGAgent.CancelGoap();
        playerAnimController.CancelAnimations(animator);
    }

    public void InDanger()
    {
        CancelFarming();
        playerGAgent.SetFear();
    }

    private void UpdateAnimator()
    {
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        GetComponent<Animator>().SetFloat("forwardSpeed", speed);
    }
}
    
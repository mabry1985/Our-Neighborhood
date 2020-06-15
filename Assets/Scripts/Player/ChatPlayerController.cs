using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class ChatPlayerController : MonoBehaviour
{
    public PlayerManager playerManager;
    public PlaceableItemManager placeableItemManager;
    public Renderer playerRenderer;
    public AnimController playerAnimController;

    public NavMeshAgent navAgent;
    public Animator animator;
    public ChatPlayerGAgent playerGAgent;
    public GInventory inventory;
    public GAction farm;
    public GAction depot;
    public Slider progressBar;
    public Transform home;

    public GameObject questionMark;
    public GameObject itemSpawnPoint;

    public static ChatPlayerController instance;
   
    public int playerID;
    public string playerName;
    public int inventorySize = 20;

    public Canvas playerNameCanvas;
    public Text playerNameText;

    private Vector3 destination;
    private float distanceToTarget;
    private bool invFull;
    
    private void Start() 
    {
        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        placeableItemManager = GameObject.Find("PlaceableItemManager").GetComponent<PlaceableItemManager>();
        depot = GetComponent<Depot>(); 
        farm = GetComponent<Farm>();
        animator = GetComponent<Animator>();
        playerGAgent = GetComponent<ChatPlayerGAgent>();
        depot.targetTag = "Home";
        home = playerManager.spawnPoint;
        playerManager.playerReferences[playerID] = this;
        inventory = new GInventory() {};
        inventory.invSpace = inventorySize;
        inventory.player = this;
        playerRenderer = gameObject.GetComponentInChildren<Renderer>();      
        playerAnimController = GetComponent<AnimController>();  
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
        if (inventory.items.Count <= inventorySize)
        {
            invFull = false;
        }
        {
            invFull = true;
        }

        UpdateAnimator();    
    }

    void LateUpdate()
    {
        if (playerNameCanvas != null)
            playerNameCanvas.transform.rotation = Camera.main.transform.rotation;
        
    }

    public void HandleFarming(string material)
    {
        CancelFarming();
        playerGAgent.CancelGoap();
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
        playerGAgent.craftingItem = craftingItem;  
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
    
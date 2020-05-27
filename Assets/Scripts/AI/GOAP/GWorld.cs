using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceQueue
{
    public Queue<GameObject> que = new Queue<GameObject>();
    public string tag;
    public string modState;

    public ResourceQueue(string t, string ms, WorldStates w)
    {
        tag = t;
        modState = ms;
        if (tag != "")
        {
            GameObject[] resources = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject r in resources)
                que.Enqueue(r);
        }

        if (modState != "" && que.Count > 0)
        {
            w.ModifyState(modState, que.Count);
        }
    }

    public void AddResource(GameObject r)
    {
        que.Enqueue(r);
    }

    public GameObject RemoveResource()
    {
        if (que.Count == 0) return null;
        return que.Dequeue();
    }
}

public sealed class GWorld
{
    private static readonly GWorld instance = new GWorld();
    private static WorldStates world;

    public static GInventory worldInventory = new GInventory();

    // private static ResourceQueue money;
    // private static ResourceQueue population;
    // private static ResourceQueue food;
    // private static ResourceQueue meds;
    // private static ResourceQueue wood;
    // private static ResourceQueue stone;
    // private static ResourceQueue iron;

    private static Dictionary<string, ResourceQueue> resources = new Dictionary<string, ResourceQueue>();

    static GWorld()
    {
        world = new WorldStates();
        worldInventory.items.Add("Money", 0);
        worldInventory.items.Add("Population", 10);
        worldInventory.items.Add("Food", 0);
        worldInventory.items.Add("Meds", 0);
        worldInventory.items.Add("Wheat", 0);
        worldInventory.items.Add("Wood", 0);
        worldInventory.items.Add("Stone", 0);
        worldInventory.items.Add("Iron", 0);

        // money = new ResourceQueue("", "", world);
        // resources.Add("money", money);
        
        // population = new ResourceQueue("", "", world);
        // resources.Add("population", population);
        
        // food = new ResourceQueue("", "", world);
        // resources.Add("food", food);
        
        // meds = new ResourceQueue("", "", world);
        // resources.Add("meds", meds);
        
        // wood = new ResourceQueue("", "", world);
        // resources.Add("wood", wood);
        
        // stone = new ResourceQueue("", "", world);
        // resources.Add("stone", stone);
        
        // iron = new ResourceQueue("", "", world);
        // resources.Add("iron", iron);
    }

    public ResourceQueue GetQueue(string type)
    {
        return resources[type];
    }

    private GWorld()
    {
    }

    public static GWorld Instance
    {
        get { return instance; }
    }

    public WorldStates GetWorld()
    {
        return world;
    }
}

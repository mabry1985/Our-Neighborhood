﻿using System.Collections;
using System.Collections.Generic;
using TwitchLib.Unity;
using TwitchLib.Client.Models;
using TwitchLib.Client.Events;
using UnityEngine;

public class GInventory
{   

    //the physical objects in the world
    public List<GameObject> worldObjects = new List<GameObject>();

    public Dictionary<string, int> items = new Dictionary<string, int>();

    public Dictionary<string, int> homeInventory = new Dictionary<string,int>();
    
    public int invSpace;

    public Player player;

    public void AddItem(GameObject i)
    {
        worldObjects.Add(i);
    }
 
    public void AddItem(string material, int amount)
    {
        items.Add(material, amount);
    }

    public GameObject FindItemWithTag(string tag)
    {
        foreach(GameObject i in worldObjects)
        {
            if(i.tag == tag)
            {
                return i;
            }
        }

        return null;
    }

    public void RemoveItem(GameObject i) 
    {
        int indexToRemove = -1;
        foreach(GameObject g in worldObjects)
        {
            indexToRemove++;
            if (g == i)
                break;
        }
        if(indexToRemove >= -1)
            worldObjects.RemoveAt(indexToRemove);
    }

    public void TransferToHomeInventory() 
    {
        List<string> keys = new List<string>(items.Keys);

        foreach (string key in keys)
        {
            Debug.Log(key + " " + items[key]);
            
            if (homeInventory.ContainsKey(key))
            {
                homeInventory[key] = homeInventory[key] += items[key];
            }
            else
            {
                homeInventory.Add(key, items[key]);
            }
            invSpace += items[key];
            items.Remove(key);
        }
            
        // foreach(KeyValuePair<string, int> item in itemsCopy)
        // {
        //     Debug.Log(item.Key);
        //     if (homeInventory.ContainsKey(item.Key)) {
        //         homeInventory[item.Key] = homeInventory[item.Key] += item.Value;
        //     }
        //     else
        //     {
        //         homeInventory.Add(item.Key, item.Value);
        //     }
        //         items.Remove(item.Key);
        //         invSpace += item.Value;
        // }
    }

    public string ListHomeInventory()
    {
        var inventory = "";
        foreach (KeyValuePair<string, int> item in homeInventory)
        {
            inventory += $"| {item.Key} = {item.Value} ";
        }
        //Debug.Log(inventory);
        
        inventory += " |";
        return inventory;
    }

    public string ListInventory()
    {
        var inventory = "";
        
        foreach (KeyValuePair<string, int> item in items)
        {
            inventory += $"| {item.Key} = {item.Value} ";
        }
        //Debug.Log(inventory);
        
        inventory += " |";
        return inventory;
    }
}

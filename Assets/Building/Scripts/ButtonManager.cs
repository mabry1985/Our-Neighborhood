﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] BuildingSO building;
    public bool canBuild;
    Button button;
    List<KeyValuePair<string, int>> bCosts = new List<KeyValuePair<string, int>>(); 

    private void Start() 
    {
        bCosts = BuildingCostHelpers.BuildCostList(building);
        button = GetComponent<Button>();    
    }

    private void FixedUpdate() 
    {
        canBuild = CanBuild();

        button.interactable = canBuild;
        
    }

    private bool CanBuild()
    {
        bool hasMats = false;
        foreach (KeyValuePair<string, int> item in bCosts)
        {
            
            if (item.Value == 0) continue;
            //print(item.Key + " " + item.Value);
            print(GWorld.worldInventory.items[item.Key] >= item.Value);
            if (GWorld.worldInventory.items[item.Key] >= item.Value)
            {
                hasMats = true;
            }
            else
            {
                return false;
            }
        }
        print(hasMats);
        return hasMats;
    }

}
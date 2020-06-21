using System.Collections;
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

    private void Update() 
    {
        canBuild = CanBuild();
        print(canBuild);

        button.interactable = canBuild;
        
    }

    private bool CanBuild()
    {
        bool hasMats = false;
        foreach (KeyValuePair<string, int> item in bCosts)
        {
            
            if (item.Value == 0) break;

            if (GWorld.worldInventory.items[item.Key] >= item.Value)
            {
                hasMats = true;
            }
            else
            {
                hasMats = false;
            }
        }
        return hasMats;
    }

}

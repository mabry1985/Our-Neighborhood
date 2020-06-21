using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class BuildingCostHelpers
{

    public static void RemoveBuildingCostsFromInv(List<KeyValuePair<string, int>> bCosts)
    {
        foreach (KeyValuePair<string,int> item in bCosts)
        {
            GWorld.worldInventory.items[item.Key] -= item.Value;
        }
    }

    public static void ReturnBuildingCostsToInv(List<KeyValuePair<string, int>> bCosts)
    {
        foreach (KeyValuePair<string,int> item in bCosts)
        {
            GWorld.worldInventory.items[item.Key] += item.Value;
        }
    }

    public static List<KeyValuePair<string, int>> BuildCostList(BuildingSO building)
    {
        List<KeyValuePair<string, int>> tempList = new List<KeyValuePair<string, int>>()
        {
            new KeyValuePair<string, int>("Wood", building.wood),
            new KeyValuePair<string, int>("Stone", building.stone),
            new KeyValuePair<string, int>("Seeds", building.seeds),
            new KeyValuePair<string, int>("Iron", building.iron),

        };

        return tempList;

    }

}

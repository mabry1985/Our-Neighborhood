using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : PlaceableBuildingBase
{

    private void OnDestroy() 
    {
        GWorld.worldInventory.items["Population"] += 1;
    }

    new public void OnPlacement()
    {
        base.OnPlacement();
        GWorld.worldInventory.items["Population"] += 1;
    }
}

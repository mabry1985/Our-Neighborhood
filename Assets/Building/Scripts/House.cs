﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{

    private void OnDestroy() 
    {
        GWorld.worldInventory.items["Population"] += 1;
    }

    public void OnPlacement()
    {
        GWorld.worldInventory.items["Population"] += 1;
    }
}

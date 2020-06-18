using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    void Start()
    {
        GWorld.worldInventory.items["Population"] += 1;
    }

    private void OnDestroy() 
    {
        GWorld.worldInventory.items["Population"] += 1;
    }
}

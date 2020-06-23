using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace : PlaceableBuildingBase
{
    ParticleSystem smoke;

    private void Awake() 
    {
        smoke = GetComponentInChildren<ParticleSystem>();
        smoke.Stop();
    }
    
    private void OnDestroy()
    {
        
    }

    new public void OnPlacement()
    {
        base.OnPlacement();
        smoke.Play();
        
    }
}

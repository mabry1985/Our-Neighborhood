using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windmill : PlaceableBuildingBase
{

    new public void OnPlacement()
    {
        base.OnPlacement();
        transform.GetChild(0).gameObject.SetActive(true);
    }
}

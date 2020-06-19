using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windmill : MonoBehaviour
{
    private void Start() 
    {

    }

    public void OnPlacement()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
}

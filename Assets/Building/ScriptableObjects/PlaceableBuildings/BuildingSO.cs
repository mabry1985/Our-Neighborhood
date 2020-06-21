using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Building", menuName = "ScriptableObject/Building", order = 1)]
public class BuildingSO : ScriptableObject
{
    public string objectName = "Building Name";
    public GameObject buildingPrefab;
    public float buildTime;
    public int wood;
    public int stone;
    public int iron;
    public int seeds;
    //public Icon icon;
    public List<KeyValuePair<string, int>> bCosts = new List<KeyValuePair<string, int>>();

    private void Awake() 
    {
        bCosts = BuildCostList(); 
        Debug.Log("building SO is awake");
    }


    private List<KeyValuePair<string, int>> BuildCostList()
    {
        List<KeyValuePair<string, int>> tempList = new List<KeyValuePair<string, int>>()
        {
            new KeyValuePair<string, int>("Wood", wood),
            new KeyValuePair<string, int>("Stone", stone),
            new KeyValuePair<string, int>("Seeds", seeds),
            new KeyValuePair<string, int>("Iron", iron),

        };

        return tempList;
    }
}

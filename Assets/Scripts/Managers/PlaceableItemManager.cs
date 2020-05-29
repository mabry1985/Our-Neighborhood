using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct PlaceableItem
{
    public GameObject Prefab;
    public int DecayTime;

    public PlaceableItem(GameObject prefab, int decayTime)
    {
        Prefab = prefab;
        DecayTime = decayTime;
    }

}


public class PlaceableItemManager : MonoBehaviour
{
    public GameObject campfirePrefab;
    public Dictionary<string, PlaceableItem> placeableItems;

    private void Start() {
        placeableItems = new Dictionary<string, PlaceableItem>()
        {
            {"Campfire", new PlaceableItem(campfirePrefab, 60)}
        };
    }
}

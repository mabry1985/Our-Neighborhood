using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GInventory
{   
    //the physical objects in the world
    public List<GameObject> worldObjects = new List<GameObject>();

    public Dictionary<string, int> items = new Dictionary<string, int>();

    public void AddItem(GameObject i)
    {
        worldObjects.Add(i);
    }
 
    public void AddItem(string material, int amount)
    {
        items.Add(material, amount);
    }

    public GameObject FindItemWithTag(string tag)
    {
        foreach(GameObject i in worldObjects)
        {
            if(i.tag == tag)
            {
                return i;
            }
        }
        
        return null;
    }

    public void RemoveItem(GameObject i) {
        int indexToRemove = -1;
        foreach(GameObject g in worldObjects)
        {
            indexToRemove++;
            if (g == i)
                break;
        }
        if(indexToRemove >= -1)
            worldObjects.RemoveAt(indexToRemove);
    }
}

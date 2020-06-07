using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinPart : MonoBehaviour
{

    public GameObject currentClothes;
    public GameObject defaultClothes;

    //public GameObject inicialSkin;

    public void SetClothes(GameObject newSKin)
    {
        currentClothes = newSKin;
        Mesh newMesh = currentClothes.GetComponent<MeshFilter>().sharedMesh;
        Material[] newMaterials = currentClothes.GetComponent<MeshRenderer>().sharedMaterials;

        GetComponent<SkinnedMeshRenderer>().sharedMesh = newMesh;
        GetComponent<SkinnedMeshRenderer>().sharedMaterials = newMaterials;

    }
    public void ResetClothes()
    {
        currentClothes = null;

        if (defaultClothes != null)
        SetClothes(defaultClothes);
        else
        {
            GetComponent<SkinnedMeshRenderer>().sharedMesh = null;
            GetComponent<SkinnedMeshRenderer>().sharedMaterials = new Material[0];
        }

    }
}

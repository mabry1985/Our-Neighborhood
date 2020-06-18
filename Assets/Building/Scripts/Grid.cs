using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameObject structure;
    Vector3 truePos;
    public float gridSize;

    void LateUpdate()
    {
        if (structure != null)
        {
            truePos.x = Mathf.Floor(structure.transform.position.x/gridSize) * gridSize;
            truePos.y = Mathf.Floor(structure.transform.position.y/gridSize) * gridSize;
            truePos.z = Mathf.Floor(structure.transform.position.z/gridSize) * gridSize;
            structure.transform.position = truePos;
        }

    }
}

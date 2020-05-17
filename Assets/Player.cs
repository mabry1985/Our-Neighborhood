using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public NavMeshAgent agent;
    public Canvas playerNameCanvas;
    public Text playerNameText;
    
    private PlayerModel playerModel = new PlayerModel();

    void Awake()
    {
        StartCoroutine(Wander());
        
        var playerRenderer = gameObject.GetComponent<Renderer>();
        playerRenderer.material.SetColor("_Color", Color.black);
    }


    void LateUpdate()
    {
        playerNameCanvas.transform.rotation = Camera.main.transform.rotation;

    }

    IEnumerator Wander() {
        while (true){
            yield return new WaitForSeconds(2);
            Vector3 newPos = RandomNavSphere(transform.position, 5, -1);
            agent.SetDestination(newPos);
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}
    
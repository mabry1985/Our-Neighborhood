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
    private JobManager jobManager;

    void Awake() 
    {
        var playerRenderer = gameObject.GetComponent<Renderer>();
        playerRenderer.material.SetColor("_Color", Color.black);

        jobManager = gameObject.GetComponent<JobManager>();
        
    }


    void LateUpdate()
    {
        playerNameCanvas.transform.rotation = Camera.main.transform.rotation;

    }


}
    
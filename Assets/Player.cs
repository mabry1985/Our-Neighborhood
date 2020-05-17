using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{

    public TwitchClient client;
    public TwitchAPI twitchAPI;

    public NavMeshAgent agent;

    public int playerID;
    public float playerXP;
    public int playerMoney;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

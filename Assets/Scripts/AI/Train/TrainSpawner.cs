using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainSpawner : MonoBehaviour
{
    public GameObject trainPrefab;
    public PlayerManager playerManager;
    public Train trainInstance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerManager.spawnedPlayerList.Count < playerManager.players.Count && this.trainInstance == null)
        {
            Instantiate(trainPrefab, transform.position, transform.rotation);
        }
    }
}

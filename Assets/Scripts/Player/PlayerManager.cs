﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform spawnPoint;

    public Dictionary<int, PlayerModel> players = new Dictionary<int, PlayerModel>();
    public List<int> spawnedPlayerList = new List<int>();
    public Dictionary<int, Player> playerReferences = new Dictionary<int, Player>();

    private Text playerName;

    public void AddToPlayersDictionary(PlayerModel player) 
    {    
        Player p = new Player();
        players.Add(player.playerID, player);
        playerReferences.Add(player.playerID, p);
        
    }

    public void PlayerSpawn(PlayerModel player) 
    {
        var go = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);


        var playerScript = go.GetComponent<Player>();
        playerScript.playerID = player.playerID;
        playerScript.playerName = player.playerName;
        playerName = go.GetComponentInChildren<Text>();
        
        playerName.text = player.playerName;
        spawnedPlayerList.Add(player.playerID);

    }
    
    public PlayerModel CreatePlayerModel(string name, int id) 
    {
        var player = new PlayerModel() {
            playerName = name,
            playerID = id,
            playerLvl = 0,
            playerMoney = 100,
            playerXP = 0,
            playerEnergy = 100,
            playerPrefab = playerPrefab
        };
        
        return player;
    }

    public IEnumerator CheckForPlayerSpawn() 
    {
        print("in check for player spawn");
        if (spawnedPlayerList.Count < players.Count) {
            foreach (KeyValuePair<int, PlayerModel> player in players) {
                    print("in check player spawn loop");
                
                if (!spawnedPlayerList.Contains((player.Key))) {
                    print("spawning player");
                    yield return new WaitForSeconds(2);
                    PlayerSpawn(player.Value);
                }
            }
        }

    }
    

    
}

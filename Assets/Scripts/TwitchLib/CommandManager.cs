using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TwitchLib.Unity;
using TwitchLib.Client.Models;

public class CommandManager : MonoBehaviour
{
    public PlayerManager playerManager;
    public BotManager botManager;

    public void CheckCommand(int id, string name, string command, List<string> arg, Client client) 
    {
        command = StringFormatter(command);
        
        for (int i = 0; i < arg.Count; i++)
        {
            arg[i] = StringFormatter(arg[i]);
        }
    
        switch (command)
        {
            case "Farm":
                playerManager.playerReferences[id].ChangeJobs("Farmer", arg[0]);
                break;
            case "Bot":
                botManager.OnBotCommand(arg);
                break;
            case "Join":
                HandleJoin(id, name, client);
                break;
            case "Ping":
                playerManager.playerReferences[id].transform.GetChild(3).gameObject.SetActive(true);
                break;
            case "Home":
                playerManager.playerReferences[id].ChangeJobs("GoHome", null);
                break;
            case "Homeinv":
                HandleHomeInv(id, client, name);
                break;
            case "Inv" :
                HandleInv(id, client, name);
                break;
            case "Cancel":
                playerManager.playerReferences[id].ChangeJobs("Idle", null);
                break;
            case "Test":
                var playerModel = playerManager.CreatePlayerModel(name, id);
                playerManager.AddToPlayersDictionary(playerModel);
                playerManager.PlayerSpawn(playerModel);
                break;
            default:
                break;
        }
    }

    private string StringFormatter(string command){
        var formatString = char.ToUpper(command[0]) + command.Substring(1);
        return formatString;
        
    }

    public void HandleJoin(int id, string name, Client client) {
        if (!playerManager.players.ContainsKey(id))
        {
            var playerModel = playerManager.CreatePlayerModel(name, id);
            playerManager.AddToPlayersDictionary(playerModel);
        }
        else
        {
            //need to get a limit increase for whispering capabilities
            //client.SendWhisper(name, $"Hi, {name}, you have already joined");
            client.SendMessage(client.JoinedChannels[0], $"Hi, {name}, you have already joined our neighborhood");
        }
    }

    public void HandleHomeInv(int id, Client client, string name) {
        var homeInv = playerManager.playerReferences[id].inventory.ListHomeInventory();
        client.SendMessage(client.JoinedChannels[0], $"{name}, at home you have {homeInv}");

    }

    public void HandleInv(int id, Client client, string name){
        var player = playerManager.playerReferences[id];
        var inventory = player.inventory;
        var items = inventory.ListInventory();
        var invSpace = inventory.invSpace;

        client.SendMessage(client.JoinedChannels[0], $"{name}, you have {invSpace} slots available");

        if (items.Length > 3)
            client.SendMessage(client.JoinedChannels[0], $"{items}");
    }

}
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
        switch (command)
        {
            case "job":
                playerManager.playerReferences[id].JobSwitch(arg);
                break;
            case "bot":
                botManager.OnBotCommand(arg);
                break;
            case "join":
                handleJoin(id, name, client);
                break;
            case "ping":
                playerManager.playerReferences[id].transform.GetChild(2).gameObject.SetActive(true);
                break;
            case "home":
                playerManager.playerReferences[id].ChangeJobs("GoHome", null);
                break;
            case "homeinv":
                handleHomeInv(id, client, name);
                break;
            case "inv" :
                handleInv(id, client, name);
                break;
            default:
                break;
        }
    }

    public void handleJoin(int id, string name, Client client) {
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

    public void handleHomeInv(int id, Client client, string name) {
        var homeInv = playerManager.playerReferences[id].inventory.ListHomeInventory();
        client.SendMessage(client.JoinedChannels[0], $"{name}, at home you have {homeInv}");

    }

    public void handleInv(int id, Client client, string name){
        var player = playerManager.playerReferences[id];
        var inventory = player.inventory;
        var items = inventory.ListInventory();
        var invSpace = inventory.invSpace;

        client.SendMessage(client.JoinedChannels[0], $"{name}, you have {invSpace} slots available");

        if (items.Length > 3)
            client.SendMessage(client.JoinedChannels[0], $"{items}");
    }

}
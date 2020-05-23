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
                if (!playerManager.players.ContainsKey(id))
                {
                    var playerModel = playerManager.CreatePlayerModel(name, id);
                    playerManager.AddToPlayersDictionary(playerModel);
                } else {
                    //need to get a limit increase for whispering capabilities
                    //client.SendWhisper(name, $"Hi, {name}, you have already joined");
                    client.SendMessage(client.JoinedChannels[0], $"Hi, {name}, you have already joined our neighborhood");
                }
                break;
            case "ping":
                playerManager.playerReferences[id].transform.GetChild(2).gameObject.SetActive(true);
                break;
            case "home":
                playerManager.playerReferences[id].ChangeJobs("GoHome", null);
                break;
            case "homeinv":
                var homeInv = playerManager.playerReferences[id].inventory.ListHomeInventory();
                client.SendMessage(client.JoinedChannels[0], $"{name}, at home you have {homeInv}");
                break;
            case "inv" :
                var player = playerManager.playerReferences[id];
                var inventory = player.inventory;
                var items = player.inventory.ListInventory();
                var invSpace = player.inventorySize;

                client.SendMessage(client.JoinedChannels[0], $"{name}, you have {invSpace} slots available");
                client.SendMessage(client.JoinedChannels[0], $"{items}");
                break;
            default:
                break;
        }
    }

}
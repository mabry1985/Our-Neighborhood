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
                playerManager.playerReferences[id].JobSwitch(arg[0]);
                break;
            case "bot":
                botManager.OnBotCommand(arg);
                break;
            case "join":
                if (!playerManager.players.ContainsKey(id))
                {
                    var player = playerManager.CreatePlayerModel(name, id);
                    playerManager.AddToPlayersDictionary(player);
                } else {
                    //need to get a limit increase for whispering capabilities
                    //client.SendWhisper(name, $"Hi, {name}, you have already joined");
                    client.SendMessage(client.JoinedChannels[0], $"Hi, {name}, you have already joined our neighborhood");
                }
                break;
            default:
                break;
        }
    }

}
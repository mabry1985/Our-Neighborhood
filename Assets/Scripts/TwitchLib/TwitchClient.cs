using System;
using System.Collections;
using System.Collections.Generic;
using TwitchLib.Unity;
using TwitchLib.Client.Models;
using UnityEngine;
using TwitchLib.Client.Events;

public class TwitchClient : MonoBehaviour
{

    public Client client;
    public PlayerManager playerManager;

    private string channelName = Secrets.CHANNEL_NAME;

    private void Start() {
        Application.runInBackground = true;

        ConnectionCredentials credentials = new ConnectionCredentials(Secrets.BOT_NAME, Secrets.BOT_ACCESS_TOKEN);
        client = new Client();
        client.Initialize(credentials, channelName);  

        client.Connect();

        client.OnRitualNewChatter += OnRitualNewChatter;
        client.OnUserJoined += OnUserJoined;
        client.OnChatCommandReceived += OnChatCommandRecieved;
        client.OnMessageReceived += OnMessageReceived;
        client.OnWhisperCommandReceived += OnWhisperCommandRecieved;

    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            print("keydown");
            client.SendMessage(client.JoinedChannels[0], "This is a test message from the bot");
        }
    }
    
    private void OnRitualNewChatter(object sender, OnRitualNewChatterArgs e)
    {
        Debug.Log(e.RitualNewChatter.RoomId);
    }


    private void OnUserJoined(object sender, OnUserJoinedArgs e)
    {   

        print(e.Username);
        if (e.Username != "our_neighborhood_bot" || e.Username != "our_neighborhood") {
            //client.SendMessage(client.JoinedChannels[0], $"Welcome, {e.Username}, type hello in the chat to join our neighborhood!");
        }
    }


    private void OnChatCommandRecieved(object sender, OnChatCommandReceivedArgs e)
    {
        var id = int.Parse(e.Command.ChatMessage.UserId);
        var arguments = e.Command.ArgumentsAsList;

        foreach (int key in playerManager.playerReferences.Keys)
        {
            print(key);
        }

        if (e.Command.CommandText == "job") {
            var player = playerManager.players[id];
            playerManager.playerReferences[id].JobSwitch(arguments[0]);
        }
    }

    private void OnWhisperCommandRecieved(object sender, OnWhisperCommandReceivedArgs e)
    {
        Debug.Log(e.Command.WhisperMessage);
        Debug.Log(e.Command.CommandText);
    }

    private void OnMessageReceived(object sender, OnMessageReceivedArgs e)
    {
        var id = int.Parse(e.ChatMessage.UserId);

        if (!playerManager.players.ContainsKey(id)) {
            var player = playerManager.CreatePlayerModel(e.ChatMessage.Username, id);
            playerManager.AddToPlayersDictionary(player); 
        };

        Debug.Log("The bot just read a message in chat");
    }


}

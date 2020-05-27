using System;
using System.Collections;
using System.Collections.Generic;
using TwitchLib.Unity;
using TwitchLib.Client.Models;
using TwitchLib.Client.Events;
using UnityEngine;

public class TwitchClient : MonoBehaviour
{

    public Client client;
    public PlayerManager playerManager;
    public CommandManager commandManager;

    private string channelName = Secrets.CHANNEL_NAME;

    private void Start() {
        commandManager = gameObject.GetComponent<CommandManager>();
        Application.runInBackground = true;
        
        InitializeClient();
    }

    private void InitializeClient() {
        ConnectionCredentials credentials = new ConnectionCredentials(Secrets.BOT_NAME, Secrets.BOT_ACCESS_TOKEN);
        client = new Client();
        client.Initialize(credentials, channelName);
        client.Connect();

        client.OnUserJoined += OnUserJoined;
        client.OnChatCommandReceived += OnChatCommandRecieved;
        client.OnMessageReceived += OnMessageReceived;
        client.OnWhisperCommandReceived += OnWhisperCommandRecieved;
    }
    
    private void OnUserJoined(object sender, OnUserJoinedArgs e)
    {   
        //print(e.Username);
        if (e.Username != "our_neighborhood_bot" && e.Username != "our_neighborhood") {
            client.SendMessage(client.JoinedChannels[0], $"Welcome, {e.Username}, type !join to catch the next train into our neighborhood");

        }
    }

    private void OnChatCommandRecieved(object sender, OnChatCommandReceivedArgs e)
    {
        var id = int.Parse(e.Command.ChatMessage.UserId);
        var name = e.Command.ChatMessage.Username;
        var arguments = e.Command.ArgumentsAsList;

        commandManager.CheckCommand(id, name, e.Command.CommandText, arguments, client);

    }

    private void OnWhisperCommandRecieved(object sender, OnWhisperCommandReceivedArgs e)
    {
        var id = int.Parse(e.Command.WhisperMessage.UserId);
        var name = e.Command.WhisperMessage.Username;
        var arguments = e.Command.ArgumentsAsList;

        commandManager.CheckCommand(id, name, e.Command.CommandText, arguments, client);
    }

    private void OnMessageReceived(object sender, OnMessageReceivedArgs e)
    {
        //Debug.Log("The bot just read a message in chat");
    }


}

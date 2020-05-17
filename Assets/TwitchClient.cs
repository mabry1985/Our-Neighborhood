using System.Collections;
using System.Collections.Generic;
using TwitchLib.Unity;
using TwitchLib.Client.Models;
using UnityEngine;
using TwitchLib.Client.Events;
using System;

public class TwitchClient : MonoBehaviour
{

    public Client client;
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
       // client.OnWhisperCommandReceived += OnWhisperCommandRecieved;

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
        Debug.Log(e.Username);
        client.SendMessage(client.JoinedChannels[0], $"Welcome, {e.Username}, type !help in the chat to learn how you can join our neighborhood!");
    }


    private void OnChatCommandRecieved(object sender, OnChatCommandReceivedArgs e)
    {
        Debug.Log(e.Command.CommandText);
        Debug.Log(e.Command.CommandIdentifier);
        var arguments = e.Command.ArgumentsAsList;

        foreach (var item in arguments)
        {
            Debug.Log(item);
        }
    }

/*
// whisper functionality temp disabled on twitch because account is too new
    private void OnWhisperCommandRecieved(object sender, OnWhisperCommandReceivedArgs e)
    {
        Debug.Log(e.Command.WhisperMessage);
        Debug.Log(e.Command.CommandText);
        Debug.Log("Argument as string" + e.Command.ArgumentsAsString.Length);
    }
*/
    private void OnMessageReceived(object sender, OnMessageReceivedArgs e)
    {
        print(e.ChatMessage.UserId);
        Debug.Log("The bot just read a message in chat");
    }


}

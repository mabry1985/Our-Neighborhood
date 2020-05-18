using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TwitchLib.Unity;
using TwitchLib.Api.Models.Undocumented.Chatters;
using System;

public class TwitchAPI : MonoBehaviour
{
    public TwitchClient twitchClient;

    public Api api;
    void Start()
    {
        Application.runInBackground = true;
        api = new Api();
        api.Settings.AccessToken = Secrets.BOT_ACCESS_TOKEN;
        api.Settings.ClientId = Secrets.CLIENT_ID;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            api.Invoke(
                api.Undocumented.GetChattersAsync(twitchClient.client.JoinedChannels[0].Channel),
                GetChatterListCallback
            );
        }
    }

    private void GetChatterListCallback(List<ChatterFormatted> listOfChatters)
    {
        Debug.Log("List of " + listOfChatters.Count + " Viewers: ");
        foreach (var chatterObject in listOfChatters) 
        {
            Debug.Log(chatterObject.Username);
        }
    }
}

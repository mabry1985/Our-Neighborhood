using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TwitchClient client;
    public TwitchAPI api;
    public PlayerManager playerManager;
    public PlaceableItemManager placeableItemManager;
    public BotManager botManager;
    public CursorManager cursorManager;

    // Start is called before the first frame update
    void Start()
    {
        Application.runInBackground = true;   
    }

    // Update is called once per frame
    void Update()
    {

    }
}

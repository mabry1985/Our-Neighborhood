using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    public PlayerManager playerManager;
    public BotManager botManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckCommand(int id, string command, List<string> arg) {
        switch (command)
        {
            case "job":
                playerManager.playerReferences[id].JobSwitch(arg[0]);
                break;
            case "bot":
                botManager.OnBotCommand(arg);
                break;
            default:
                break;
        }
    }

}
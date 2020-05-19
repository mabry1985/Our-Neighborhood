using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotManager : MonoBehaviour
{
    public GameObject botPrefab;

    public Dictionary<int, BotModel> bots = new Dictionary<int, BotModel>();
    public List<int> spawnedBotList = new List<int>();

    private Text botName;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckForBotSpawn());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddToBotsDictionary(BotModel bot)
    {
        bots.Add(bot.botID, bot);


    }

    public void BotSpawn(BotModel bot)
    {
        var go = Instantiate(botPrefab, transform.position, transform.rotation);


        var botScript = go.GetComponent<Bot>();
        botScript.botID = bot.botID;
        botScript.botName = bot.botName;
        print("in bot spawn" + botScript.botID);
        botName = go.GetComponentInChildren<Text>();

        botName.text = bot.botName;
        spawnedBotList.Add(bot.botID);

    }

    public BotModel CreateBotModel(string name, int id)
    {
        var bot = new BotModel()
        {
            botName = name,
            botID = id,
            botLvl = 0,
            botMoney = 100,
            botXP = 0,
            botEnergy = 100,
            botPrefab = botPrefab
        };

        return bot;
    }

    public IEnumerator CheckForBotSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);

            if (spawnedBotList.Count < bots.Count)
            {
                foreach (KeyValuePair<int, BotModel> bot in bots)
                {
                    if (!spawnedBotList.Contains((bot.Key)))
                    {
                        BotSpawn(bot.Value);
                    }
                }

            }

        }
    }


}
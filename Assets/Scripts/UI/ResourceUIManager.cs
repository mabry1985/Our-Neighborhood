using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ResourceUIManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI money;
    public TMPro.TextMeshProUGUI population;
    public TMPro.TextMeshProUGUI food;
    public TMPro.TextMeshProUGUI water;
    public TMPro.TextMeshProUGUI grain;
    public TMPro.TextMeshProUGUI seed;
    public TMPro.TextMeshProUGUI wood;
    public TMPro.TextMeshProUGUI stone;
    public TMPro.TextMeshProUGUI iron;

    public Canvas resourceOverlay;
    public PlayerManager playerManager;
    public List<int> spawnedPlayers;

    private Dictionary<string, int> worldInventory;


    // Start is called before the first frame update
    void Start()
    {
        worldInventory = GWorld.worldInventory.items;
        spawnedPlayers = playerManager.spawnedPlayerList;
    }

    // Update is called once per frame
    void Update()
    {
        money.text = $"$ {worldInventory["Money"]}";
        population.text = $"Pop: {spawnedPlayers.Count}/{worldInventory["Population"]}";
        food.text = $"Food: {worldInventory["Food"]}";
        water.text = $"Water: {worldInventory["Water"]}";
        grain.text = $"Wheat: {worldInventory["Grain"]}";
        grain.text = $"Seeds: {worldInventory["Seeds"]}";
        wood.text = $"Wood: {worldInventory["Wood"]}";
        stone.text = $"Stone: {worldInventory["Stone"]}";
        iron.text = $"Iron: {worldInventory["Iron"]}";
    }
}

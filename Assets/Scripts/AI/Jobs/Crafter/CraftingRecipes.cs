using System;
using System.Collections.Generic;
using System.Collections;

public static class CraftingRecipes
{
    public static readonly List<KeyValuePair<string, int>> materials;
    public static readonly Dictionary<string, List<KeyValuePair<string, int>>> recipes;

    static CraftingRecipes()
    {
        recipes = new Dictionary<string, List<KeyValuePair<string, int>>>();
        materials = new List<KeyValuePair<string, int>>()
        {
            new KeyValuePair<string, int>("Wood", 10),
            new KeyValuePair<string, int>("Stone", 5)
        };
        recipes.Add("Campfire", materials);
        
        materials = new List<KeyValuePair<string, int>>()
        {
            new KeyValuePair<string, int>("Water", 10),
            new KeyValuePair<string, int>("Wheat", 5)
        };

        recipes.Add("Bread", materials);


    }
}

using TMPro;
using UnityEngine.UI;

public static class PlayerStats
{

    //TODO: Make this the entire character stat and skill screen for all games. :D


    public static void Setup() 
    {
        // Load Player Data
        QiCount.Initiate(10, 10);
        SystemPointsCount.Initiate(0);
        ulong[] temp = new ulong[1];
        temp[0] = 0;
        SlagCount.Initiate(temp);

        Load();

        SaveLoad.SaveCheck();

        // This is a debug line more than anything for now.
        // I use it to check the state on bootup.
        SaveLoad.Save();
    }

    private static void Load() 
    {
        // TODO: make this read from the saves files.
        // That will have to wait for when I make a main menu for the game...
        new QiConvert(0, 0);
        new QiRegen(0, 0);
        new QiPurity(0, 0);
    }

    public static void Display() 
    {
        SystemPointsCount.Display();
        QiCount.Display();
        SlagCount.DisplayAll();
        // Add any future stat displays here for updating.
        // This is primarily used for bootup display.
    }

    /// <summary>
    /// Generates the JSON string representing the stats of the character.
    /// </summary>
    /// <returns> The JSON representation of player stats. </returns>
    public static string SerializeStats() 
    {
        string json = "Stats:[";
        // Fill with stats
        json += QiCount.ToJson() + ",";
        json += SlagCount.ToJson() + ",";
        json += SystemPointsCount.ToJson();
        return json + "]";
    }
}

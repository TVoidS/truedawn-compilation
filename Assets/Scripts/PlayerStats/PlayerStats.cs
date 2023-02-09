using System.Text.Json;
using TMPro;
using UnityEngine.UI;

public static class PlayerStats
{

    //TODO: Make this the entire character stat and skill screen for all games. :D

    public static void Setup(TextMeshProUGUI qiCountDisplay, TextMeshProUGUI slagCount, TextMeshProUGUI sysDisp, Button slagSell) 
    {
        // Load Player Data
        QiCount.Initiate(10, qiCountDisplay, 10);
        SystemPointsCount.Initiate(sysDisp, 0);
        SlagCount.Initiate(slagCount,slagSell, 0);
        SystemPointsCount.Display();
    }

    private static void Load() 
    {
        // Make this load from PlayerStats file
        new QiRegen(0, 0);
        new QiConvert(0, 0);

        // TODO: Fix.
    }

    private static void Save() 
    {

    }

    /* Waiting for an update for 
    private static void SaveToFile(string filePath, object obj)
    {
        var options = new JsonSerializerOptions();

        string json = JsonSerializer.Serialize(obj, options);
        File.WriteAllText(filePath, json);
    }
    */
}

using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Text.Json;

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

        SaveCheck();
    }

    private static void Load() 
    {
        // Make this load from PlayerStats file
        //new QiRegen(0, 0);
        //new QiConvert(0, 0);

        // TODO: Create
    }

    private static readonly string SaveLoc = Application.dataPath + Path.DirectorySeparatorChar + "saves";
    
    public static void Save() 
    {
        string json = "{\"Skills\":" + SkillController.SerializeSkills() + "}";
        JsonSerializerOptions options = new() { WriteIndented = true };

        json = JsonSerializer.Serialize(json, options);
        // Save to the save1.json file in the saves directory.
        // TODO: make this more files.
        File.WriteAllText(SaveLoc + Path.DirectorySeparatorChar + "save1.json", json);

    }

    /// <summary>
    /// Verifies the existence of the Save directory, and if it doesn't exist, creates it.
    /// </summary>
    private static void SaveCheck() 
    {
        if (Directory.Exists(SaveLoc))
        {
            // Save locations exist, we good.
        }
        else 
        {
            Directory.CreateDirectory(SaveLoc);
        }
    }
}

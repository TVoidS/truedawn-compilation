using System.IO;
using System.Text.Json;
using UnityEngine;

public static class SaveLoad
{
    public static string Save(object data) 
    {
        return JsonSerializer.Serialize(data);
    }

    public static string Prettify(string json) 
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        return JsonSerializer.Serialize(json, options);
    }

    /// <summary>
    /// Verifies the existence of the Save directory, and if it doesn't exist, creates it.
    /// </summary>
    public static void SaveCheck()
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

    /// <summary>
    /// The save file location
    /// </summary>
    private static readonly string SaveLoc = Application.dataPath + Path.DirectorySeparatorChar + "saves";

    /// <summary>
    /// Saves the game.  TODO: finish this.  It requires double checking everything works and all that.
    /// And loading.  that too.
    /// </summary>
    public static void Save()
    {
        // TODO: Add quest saving here!
        string json = "{"
            + SkillController.SerializeSkills() + ","
            + PlayerStats.SerializeStats()
            + "}";

        json = Prettify(json);

        // Save to the save1.json file in the saves directory.
        // TODO: make this more files.
        File.WriteAllText(SaveLoc + Path.DirectorySeparatorChar + "save1.json", json);
    }
}

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

    private static string SaveCheck(string saveName) 
    {
        int count = 0;
        string saveAddon = "";
        while (!File.Exists(SaveLoc + Path.DirectorySeparatorChar + saveName + saveAddon + ".json")) 
        {
            saveAddon = "" + count;
            count++;
        }
        return SaveLoc +  Path.DirectorySeparatorChar + saveName + saveAddon + ".json";
    }

    /// <summary>
    /// The save file location
    /// </summary>
    private static readonly string SaveLoc = Application.dataPath + Path.DirectorySeparatorChar + "saves";

    /// <summary>
    /// Saves the game.  TODO: finish this.  It requires double checking everything works and all that.
    /// And loading.  that too.
    /// </summary>
    public static void Save(string saveName)
    {
        // Save to the save1.json file in the saves directory.
        // TODO: make this more files.
        File.WriteAllText(SaveLoc + Path.DirectorySeparatorChar + saveName + ".json", JsonGenerate());
    }

    /// <summary>
    /// Creates a new Save
    /// </summary>
    /// <param name="saveName"></param>
    public static void NewSave(string saveName)
    {
        // Save to the save1.json file in the saves directory.
        // TODO: make this more files.
        File.WriteAllText(SaveCheck(saveName), JsonGenerate());
    }

    private static string JsonGenerate()
    {
        // TODO: Add quest saving here!
        string json = "{"
            + SkillController.SerializeSkills(1) + ","
            + PlayerStats.SerializeStats(1)
            + "\n}";

        return json;
    }

    /// <summary>
    /// Returns the list of all .json files in the saves folder.
    /// This does no other verification on the file other than that it is a json in the correct folder.
    /// </summary>
    /// <returns> The array of .json files in the Saves folder. </returns>
    public static string[] SavedFiles() 
    {
        return Directory.GetFiles(SaveLoc,".json");
    }
}
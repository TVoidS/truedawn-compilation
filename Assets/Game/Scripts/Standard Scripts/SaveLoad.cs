using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    
    /// <summary>
    /// Returns a list of SaveData structs for the purpose of generating and feeding the Prefabs of the Save Rows in the Load Game and Save Game screens.
    /// </summary>
    /// <returns> The List of SaveData structs. </returns>
    public static List<SaveData> LoadSaveData() 
    {
        List<SaveData> saves = new List<SaveData>();

        // The list of saved files
        string[] files = SavedFiles();

        // Run the code for each save
        foreach (string file in files) 
        {
            // Create a new empty struct for the data to be transferred.
            SaveData data = new SaveData();
            data.Path = file;

            // Save the last write time to the struct
            data.LastSaveTime = File.GetLastWriteTime(file);

            // Read and parse the save file to a JSON Document
            JsonDocument doc = JsonDocument.Parse(File.ReadAllText(file));

            // Parse the Name from the root.
            data.Name = doc.RootElement.GetProperty("Name").GetString();

            //TODO: Determine highest slag tier
            //TODO: Determine quantity of highest slag tier

            // Parse each skill in the save for it's Qi purity tier and grade.
            foreach (JsonElement skill in doc.RootElement.GetProperty("Skills").EnumerateArray()) 
            {
                JsonElement curr = skill.GetProperty("ID");
                if (curr.GetString().Equals("QiPurity")) 
                {
                    data.PurityGrade = curr.GetProperty("Level").GetByte();
                    data.PurityTier = curr.GetProperty("Rank").GetByte();
                    
                    // Delete this if I end up needing more data from other skills.
                    break;
                }
            }

            saves.Add(data);
        }

        return saves;
    }

    /// <summary>
    /// Checks for the existence of a specific file
    /// </summary>
    /// <param name="name"> The character name. Don't include the extension. </param>
    /// <returns> True if the save exists, False if it doesn't. </returns>
    internal static bool SaveExists(string name)
    {
        if (SavedFiles().Contains(name + ".json")) 
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

/// <summary>
/// This is to be handed to the code attatched to the save slot prefab so that the onClick can render it on the SaveRundown object.
/// </summary>
public struct SaveData 
{
    public string Name;

    public string Path;
    
    public DateTime LastSaveTime;
    
    public SlagTypes HighestSlagTier;

    /// <summary>
    /// This is only for the highest tier
    /// </summary>
    public ulong SlagQuantity; // This is only for the highest tier.
    
    public byte PurityTier;
    
    public byte PurityGrade;
    
    // TODO: Add data for stuff that will be displayed so that I can get the loading screen to display it.
}

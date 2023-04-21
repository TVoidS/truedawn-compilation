using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using UnityEngine;

public static class SaveLoad
{
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
    /// Saves the game to the current character's Name.json
    /// </summary>
    public static void Save() 
    {
        File.WriteAllText(SaveLoc + Path.DirectorySeparatorChar + PlayerStats.Name + ".json", JsonGenerate());
    }
    
    /// <summary>
    /// Builds the JSON formatted string from the game statistics.
    /// </summary>
    /// <returns> The JSON formatted save string. </returns>
    private static string JsonGenerate()
    {
        // NOTE: Add quest saving here!
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
        string[] ret = Directory.GetFiles(SaveLoc, "*.json");
        return ret;
    }
    
    /// <summary>
    /// Returns a list of SaveData structs for the purpose of generating and feeding the Prefabs of the Save Rows in the Load Game and Save Game screens.
    /// </summary>
    /// <returns> The List of SaveData structs. </returns>
    public static List<SaveData> GetSaveDatas() 
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
                if (skill.GetProperty("ID").GetString().Equals("QiPurity")) 
                {
                    data.PurityGrade = skill.GetProperty("Level").GetByte();
                    data.PurityTier = byte.Parse(skill.GetProperty("Rank").GetRawText());
                    
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

    /// <summary>
    /// The SaveData struct for the current game.
    /// This is used to show the current run-down of the game on the save screen primarily, but can be used anywhere this data may be needed.
    /// </summary>
    /// <returns> The SaveData struct representing the current playdata. </returns>
    public static SaveData CurrentSaveDetails() 
    {
        SaveData data = new SaveData();
        
        // Basic data retrieval
        data.Name = PlayerStats.Name;
        data.HighestSlagTier = SlagCount.GetHighestTier();
        data.SlagQuantity = SlagCount.GetSlagQuantity(data.HighestSlagTier);

        try
        {
            // Must obtain the QiPurity skill data to retrieve both Rank and Level.
            ILevelable purity = (ILevelable)SkillController.GetSkill(SkillEnums.Skill.QiPurity);
            data.PurityTier = purity.Rank;
            data.PurityGrade = purity.Level;
        }
        catch
        {
            data.PurityTier = 0;
            data.PurityGrade = 0;
        }

        // The last time the game was saved.  This is likely to be right, but isn't guaranteed.
        data.LastSaveTime = File.GetLastWriteTime(SaveLoc + Path.DirectorySeparatorChar + PlayerStats.Name + ".json");

        return data;
    }

    /// <summary>
    /// Loads the JSON file at the given path.
    /// It is recommended to be used in tandem with the SaveData object's Path variable, as that is exactly the data expected.
    /// </summary>
    /// <param name="path"> The string representation of the file path for the save to be loaded. </param>
    public static void LoadSave(string path) 
    {
        JsonElement root = JsonDocument.Parse(File.ReadAllText(path)).RootElement;
        root.GetProperty("Skills");

        PlayerStats.LoadStats(root.GetProperty("Stats"));
        PlayerStats.LoadSkills(root.GetProperty("Skills"));
        PlayerStats.SetName(root.GetProperty("Name").GetString());
    }

    /// <summary>
    /// Starts the game with the basic stats rather than loaded stats.
    /// </summary>
    public static void NewGame() 
    {
        // nothing?
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
    
    // NOTE: Add data for stuff that will be displayed so that I can get the loading screen to display it.
}

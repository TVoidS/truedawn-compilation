using System.Text.Json;

public static class PlayerStats
{

    //TODO: Make this the entire character stat and skill screen for all games. :D
    public static string Name = "";

    public static void Setup() 
    {
        // Initiate the objects and load their default values.
        LoadStats();
        LoadSkills();

        SaveLoad.SaveCheck();
    }

    public static void Setup(string savePath)
    {
        // Initiate the objects and load their default values.
        LoadStats();
        LoadSkills();

        SaveLoad.SaveCheck();

        // Load in the correct values to the game, as per the save file.
        SaveLoad.LoadSave(savePath);
    }

    private static void LoadSkills() 
    {
        new QiConvert(0, 0);
        new QiRegen(0, 0);
        new QiPurity(0, 0);
    }

    public static void LoadSkills(JsonElement skills) 
    {
        // TODO: Finish
    }

    public static void LoadStats() 
    {
        // Load Player Data
        QiCount.Initiate(10, 10);
        SystemPointsCount.Initiate(0);
        SlagCount.Initiate();
    }

    public static void LoadStats(JsonElement stats) 
    {
        // TODO:
        JsonElement.ArrayEnumerator stat = stats.EnumerateArray();
        do 
        {
            switch (stat.Current.GetProperty("Stat").GetRawText()) 
            {
                case "Slag": SlagCount.Load(stat.Current.GetProperty("Slag"));  break;
                case "SP": SystemPointsCount.Load(stat.Current); break;
                case "Qi": QiCount.Load(stat.Current); break;
            }
        } while (stat.MoveNext());
    }

    /// <summary>
    /// Sets the name for the character.
    /// This is for the save filename, and for display in-game.
    /// </summary>
    /// <param name="name"> The character's new name. </param>
    public static void SetName(string name) 
    {
        if (name == "") 
        {
            Name = name;
        }
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
    /// <param name="tabcount"> The number of \t tabs to include before every line. </param>
    /// <returns> The JSON representation of player stats. </returns>
    public static string SerializeStats(byte tabcount)
    {
        string tabs = "";
        for (byte i = 0; i < tabcount; i++) 
        {
            tabs += "\t";
        }

        string json = "\n"+tabs+"\"Name\":"+Name+","+
            "\n"+tabs+"\"Stats\":[\n";
        // Fill with stats
        json += QiCount.ToJson((byte)(tabcount + 1)) + ",\n";
        json += SlagCount.ToJson((byte)(tabcount + 1)) + ",\n";
        json += SystemPointsCount.ToJson((byte)(tabcount + 1));
        return json + "\n" + tabs + "]";
    }

    /// <summary>
    /// Sets the Save Name for the game.
    /// This determines the filename for the save as well.
    /// </summary>
    /// <param name="name"> The Name of the Character/Save File. </param>
    public static bool SetNewName(string name) 
    {
        if (SaveLoad.SaveExists(name))
        {
            return false;
        }
        else 
        {
            Name = name;
            return true;
        }
    }
}

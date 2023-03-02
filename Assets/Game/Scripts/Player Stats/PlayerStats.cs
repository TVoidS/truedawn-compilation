using System.Linq;

public static class PlayerStats
{

    //TODO: Make this the entire character stat and skill screen for all games. :D
    private static string save = "";

    public static void Setup() 
    {
        // Load Player Data
        QiCount.Initiate(10, 10);
        SystemPointsCount.Initiate(100);
        ulong[] temp = new ulong[1];
        temp[0] = 0;
        SlagCount.Initiate(temp);

        Load();

        SaveLoad.SaveCheck();

        // This is a debug line more than anything for now.
        // I use it to check the state on bootup.
        SaveLoad.Save("autosave");
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
    /// <param name="tabcount"> The number of \t tabs to include before every line. </param>
    /// <returns> The JSON representation of player stats. </returns>
    public static string SerializeStats(byte tabcount)
    {
        string tabs = "";
        for (byte i = 0; i < tabcount; i++) 
        {
            tabs += "\t";
        }

        string json = "\n"+tabs+"\"Stats\":[\n";
        // Fill with stats
        json += QiCount.ToJson((byte)(tabcount + 1)) + ",\n";
        json += SlagCount.ToJson((byte)(tabcount + 1)) + ",\n";
        json += SystemPointsCount.ToJson((byte)(tabcount + 1));
        return json + "\n" + tabs + "]";
    }

    /// <summary>
    /// Sets the file to be loaded by SaveLoad when the game is about to begin.
    /// An empty string as the target sets the system to load its default state, making a new game.
    /// </summary>
    /// <param name="target"> The save that will be loaded. </param>
    public static bool SetLoadFile(string target) 
    {
        if (target == "")
        {
            // New Game.
            return true;
        }
        else 
        {
            if (SaveLoad.SavedFiles().Contains<string>(target))
            {
                save = target;
                return true;
            }
            else 
            {
                // This shouldn't happen, but it is here if it does.
                return false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public static class SlagCount
{
    private static ulong[] Slags = new ulong[Enum.GetNames(typeof(SlagTypes)).Length];

    private static readonly List<ISlagTextDisplay> SlagDisplays = new();

    private static bool initiated = false;

    /// <summary>
    /// Simplified Initiator for setting up the data.
    /// This version will likely be deprecated soon, as I have no reason to load in this data during initialization. 
    /// </summary>
    /// <param name="slag"> The array of slag to initialize to. </param>
    /// <returns> true if it initializes, false if it failed to. </returns>
    public static bool Initiate(ulong[] slag)
    {
        if(!initiated)
        {
            // Default Slag:
            for (int i = 0; i < Slags.Length; i++) 
            {
                try
                {
                    Slags[i] = slag[i];
                }
                catch 
                {
                    Slags[i] = 0;
                }
            }
            initiated = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Simplified Initiator for setting up the data
    /// </summary>
    /// <returns> true if it initializes, false if it failed to. </returns>
    public static bool Initiate() 
    {
        if(!initiated)
        {
            for (int i = 0; i < Slags.Length; i++) 
            {
                Slags[i] = 0;
            }
            initiated = true;
            return true;
        }
        else 
        {
            return false; 
        }
    }

    /// <summary>
    /// This should work for reading data from the input JsonElement array and loading it into the Slags array
    /// </summary>
    /// <param name="SlagArray"> The JsonElement that represents the Slag array in the save file. </param>
    public static void Load(JsonElement SlagArray) 
    {
        JsonElement.ArrayEnumerator slagEnum = SlagArray.EnumerateArray();
        int i = 0;
        while (slagEnum.MoveNext())
        {
            Slags[i] = slagEnum.Current.GetUInt64();
            i++;
        }
    }

    /// <summary>
    /// Adds a given amount of slag, of a given type.
    /// No other frills.  Really.
    /// </summary>
    /// <param name="slagAdd"> The amount of slag to add. </param>
    /// <param name="type"> </param>
    /// <returns></returns>
    public static bool Add(ulong slagAdd, SlagTypes type)
    {
        if (Slags[(int)type] + slagAdd < Slags[(int)type])
        {
            Slags[(int)type] = ulong.MaxValue;
        }
        else
        {
            Slags[(int)type] += slagAdd;
        }
        Display(type);
        return true;
    }

    /// <summary>
    /// Removes a certain amount of slag of a certain type.
    /// This is not likely to need to happen, but will be used for any situation that would reduce the amount of slag OTHER than selling the slag.
    /// </summary>
    /// <param name="slagSub"> The amount of slag to be removed. </param>
    /// <param name="type"> The type of slag being removed. </param>
    /// <returns></returns>
    public static bool Sub(ulong slagSub, SlagTypes type)
    {
        if (Slags[(int)type] - slagSub > Slags[(int)type])
        {
            return false;
        }
        else
        {
            Slags[(int)type] -= slagSub;
            Display(type);
            return true;
        }
    }

    /// <summary>
    /// Only updates displays associated with a given slag type.
    /// </summary>
    /// <param name="type"> The type of slag that needs updating. </param>
    private static void Display(SlagTypes type)
    {
        SlagDisplays.ForEach(delegate (ISlagTextDisplay x) {
            if (x.SlagType == type) 
            {
                x.SetText(Slags[(int)type] + " g");
            }
        });
    }

    /// <summary>
    /// Updates all documented displays.
    /// </summary>
    public static void DisplayAll() 
    {
        SlagDisplays.ForEach(delegate (ISlagTextDisplay x) {
            x.SetText(Slags[(int)x.SlagType] + " g");
        });
    }

    /// <summary>
    /// Registers a display to be updated when any data changes. 
    /// </summary>
    /// <param name="display"> The display to be updated. </param>
    public static void RegisterDisplay(ISlagTextDisplay display) 
    {
        SlagDisplays.Add(display);
    }

    /// <summary>
    /// Attempts to sell the slag, if we have enough to sell.
    /// </summary>
    /// <param name="type"> The type of slag being sold. </param>
    public static void SlagSell(SlagTypes type)
    {
        if (Slags[(int)type] > 100)
        {
            ulong slag = Slags[(int)type];
            Slags[(int)type] %= 100;
            slag -= Slags[(int)type];
            SystemPointsCount.Add(Convert(slag, type));
            Display(type);
        }
        else 
        {
            SkillController.Log("Not Enough Slag");
        }
    }

    /// <summary>
    /// Complex formula, or not.
    /// This is in charge of the value of SP we get back from converting any given amount of any given type of slag
    /// </summary>
    /// <param name="slag"> The amount of slag being converted. </param>
    /// <param name="type"> The type of slag being converted. </param>
    /// <returns></returns>
    private static ulong Convert(ulong slag, SlagTypes type) 
    {
        return (ulong)(slag*Math.Pow(10,((int)type)-1));
        // Math.Pow             // Run exponential growth based on Type
        // 10                   // We are multiplying by a specific amount of 10.
        // (uint)type           // get the integer representation of the type
        //              This will be 0 for the lowest setting of Slag
        // -1                   // Offset the power by -1 so that we are dividing by 10 at the lowest level.
        // This results in each tier selling for 10 times more than the last per gram.
    }

    /// <summary>
    /// Converts the object to a JSON formatted string, with the correct amount of tabbing.
    /// This is automatically prettified compared to normal JSON serialization.
    /// If you want to contain this in a normal JSON file without any other data, make sure to encase it in {}.
    /// </summary>
    /// <param name="tabcount"> The number of tabs to be included before every line. </param>
    /// <returns> The JSON formatted string representing the object data. </returns>
    public static string ToJson(byte tabcount) 
    {
        string tabs = "";
        for (byte i = 0; i < tabcount; i++)
        {
            tabs += "\t";
        }

        string json = "";

        foreach (SlagTypes type in typeof(SlagTypes).GetEnumValues()) 
        {
            json += ",\n" 
                + tabs + "\t\t" + Slags[(int)type];
        }

        json = "\n" 
            + tabs + "\t\"Slag\":[" 
            + json[1..] + "\n" 
            + tabs + "\t]";

        return tabs + "{\n" 
            + tabs + "\t\"Stat\":\""+ StatEnums.Slag +"\","
            + json + "\n" 
            + tabs +"}";
    }

    /// <summary>
    /// Simply returns the SlagType enumerator representing the highest tier of Slag that the player has more than 0 grams of.
    /// </summary>
    /// <returns> The highest tier of slag that the player posesses, as a SlagTypes enumerator. </returns>
    public static SlagTypes GetHighestTier() 
    {
        SlagTypes tier = SlagTypes.InferiorSlag;
        
        for (int i = 0; i < Slags.Count(); i++) 
        {
            if (Slags[i] > 0) 
            {
                tier = (SlagTypes) i;
            }
        }

        return tier;
    }

    /// <summary>
    /// Simply returns the quantity of slag for the given type.
    /// </summary>
    /// <param name="slagType"> The type of slag that you need to know the amount for. </param>
    /// <returns> The quantity of slag that the player has for the provided type. </returns>
    public static ulong GetSlagQuantity(SlagTypes slagType) 
    {
        return Slags[(int)slagType];
    }
}

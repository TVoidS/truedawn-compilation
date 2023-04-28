using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using UnityEngine;

public static class QuestManager
{
    /// <summary>
    /// The folder for all of the quest files.
    /// </summary>
    private static readonly string SaveLoc = Application.dataPath + Path.DirectorySeparatorChar + "quests";

    /// <summary>
    /// The list of all quests that have 
    /// </summary>
    private static readonly List<Quest> Quests = new();

    /// <summary>
    /// Checks the collection of Quests to see if any of them are triggered or completed by the new eventTrigger count.
    /// Please remember that the count may or may not be the number of occurances. 
    /// </summary>
    /// <param name="eventTrigger"> The event that was called. </param>
    /// <param name="count"> The count of the event, to see if it has occured enough. </param>
    public static void check(string eventTrigger) 
    {
        Quests.ForEach(quest => 
        {
            if (quest.ContainsKey(eventTrigger))
            {
                // TODO: Complete these being added to a display or something.
                if (quest.CheckTriggers())
                {
                    // It has completed the triggers, activate the quest in display and other locations
                }
                    
                // This is separate in case of the rare occurance where it is immediately completed.
                if (quest.CheckRequirements()) 
                {
                    // It has met the completion requirements
                }
            }
        });
    }

    public static void Load(JsonElement PlayerQuestJson) 
    {
        Quests.Clear();
        // TODO: convert player save data into completed quests
    }

    /// <summary>
    /// Returns the beautified json formatted save string with the appropriate amount of tabs built in.
    /// </summary>
    /// <param name="tabcount"> The number of tabs to include in the format. </param>
    public static string Save(byte tabcount)
    {
        string ret = string.Empty;
        // TODO: Build up the Save logic.
        return ret;
    }
}

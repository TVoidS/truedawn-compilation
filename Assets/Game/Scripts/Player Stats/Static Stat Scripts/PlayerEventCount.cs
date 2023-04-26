using System.Collections.Generic;
using System.Text.Json;

public static class PlayerEventCount
{
    private static readonly Dictionary<string, ulong> EventCounts = new();

    public static void Load(JsonElement events) 
    {
        ClearEvents();
        // Recieve the JSON Node for the Events
        JsonElement.ObjectEnumerator occurance = events.EnumerateObject();
        while (occurance.MoveNext()) 
        {
            RegisterEvent(occurance.Current.Name, occurance.Current.Value.GetUInt64());
        }
    }

    public static void RegisterEvent(string eventTag, ulong value) 
    {
        if (EventCounts.ContainsKey(eventTag))
        {
            EventCounts.Add(eventTag, value);
        }
        else
        {
            EventCounts[eventTag] += value;
        }
        // TODO: Send message to Quest system to see if a new quest is unlocked
    }

    private static void ClearEvents() 
    {
        EventCounts.Clear();
    }

    public static string Save(byte tabcount)
    {
        string tabs = "";
        for (byte i = 0; i < tabcount; i++)
        {
            tabs += "\t";
        }

        return tabs + "\n\"Events\": {\n"
            + Serialize(tabs)
            + tabs + "}";
    }

    private static string Serialize(string tabs) 
    {
        string ret = string.Empty;
        foreach(string key in EventCounts.Keys) 
        {
            ret += ",\n"+ tabs + "\t\"" + key + "\":" + EventCounts[key];
        }
        return ret[1..] + "\n";
    }
}
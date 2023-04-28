using System.Collections.Generic;
using System.Data;

public class Quest
{
    private readonly Dictionary<string, ulong> triggers = new();
    private readonly Dictionary<string, ulong> requirements = new();
    private bool Complete = false;
    private bool Active = false;

    public Quest() { }

    /// <summary>
    /// Add a trigger to the quest that will allow it to become active.
    /// It will be stored in a dictionary.
    /// </summary>
    /// <param name="EventCode"> The event code as listed in the PlayerEventCount object. </param>
    /// <param name="Count"> The count of the PlayerEventCount of the given code required to trigger the Quest. </param>
    /// <exception cref="ReadOnlyException"> Thrown if you try to modify a trigger that is already created. </exception>
    public void AddTrigger(string EventCode, ulong Count)
    {
        if (!triggers.ContainsKey(EventCode))
        {
            triggers.Add(EventCode, Count);
        }
        else
        {
            throw new ReadOnlyException();
        }
    }

    public void AddRequirement(string EventCode, ulong Count)
    {
        if (!requirements.ContainsKey(EventCode))
        {
            requirements.Add(EventCode, Count);
        }
        else
        {
            throw new ReadOnlyException();
        }
    }

    public bool CheckTriggers()
    {
        if (Active)
        {
            return false;
        }
        else
        {
            bool result = true;
            Dictionary<string, ulong>.Enumerator trigger = triggers.GetEnumerator();
            while (trigger.MoveNext())
            {
                if (!(PlayerEventCount.GetEventCount(trigger.Current.Key) >= trigger.Current.Value))
                {
                    // Not enough occurances
                    result = false; break;
                }
            }
            if (result) 
            {
                Active = true;
            }
            return result;
        }
    }

    public bool CheckRequirements()
    {
        if (Complete)
        {
            return false;
        }
        else
        {
            bool result = true;
            Dictionary<string, ulong>.Enumerator requirement = requirements.GetEnumerator();
            while (requirement.MoveNext())
            {
                if (!(PlayerEventCount.GetEventCount(requirement.Current.Key) >= requirement.Current.Value))
                {
                    // Not enough occurances
                    result = false; break;
                }
            }
            if (result) 
            {
                Complete = true;
            }
            return result;
        }
    }

    public bool ContainsKey(string eventTag) 
    {
        if (Complete)
        {
            return false;
        }
        else
        {
            return (triggers.ContainsKey(eventTag) || requirements.ContainsKey(eventTag));
        }
    }

    public bool IsComplete() 
    {
        return Complete;
    }

    public bool IsActive() 
    {
        return Active;
    }
}
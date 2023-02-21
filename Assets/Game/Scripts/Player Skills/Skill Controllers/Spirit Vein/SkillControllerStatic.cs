using System;
using System.Collections.Generic;
using UnityEngine;

public partial class SkillController : MonoBehaviour
{
    // STATIC SECTION:

    // TODO: Make this thread-safe!
    // The exception relating to the RunTimerSkills() method is due to it deleting itself while it is looking at itself!

    /// <summary>
    /// The list of all skills that are in need of the Update() function.
    /// This item needs to be ThreadSafe soon.
    /// </summary>
    private static readonly List<ITimerSkill> ActiveTimerSkills = new();

    /// <summary>
    /// The unchanging list of EventListeners for the Animation Triggers.
    /// This does NOT need to be threadsafe, as there will not be any removal events probably.
    /// </summary>
    private static readonly List<AnimTriggerCallback> AnimTriggers = new();

    /// <summary>
    /// The list of all skills.  This should NEVER have anything removed from it, and it should not have more than one of each skill.
    /// As this will not have any removal, there is no need for being thread-safe.
    /// </summary>
    private static readonly List<Skill> SkillList = new();

    /// <summary>
    /// The list of all Text Displays.
    /// Use this to update any text display that has been registered in the event handler.
    /// </summary>
    private static readonly List<IGeneralizedTextDisplay> TextDisplays = new();

    /// <summary>
    /// Adds the skill to the list that will be ran over every frame by Unity.
    /// </summary>
    /// <param name="skill"> The skill to be Updated every frame. This should usually be "this" but can be other skills if a skill triggers multiple things. </param>
    /// <returns> Success state, true or false, always true for now. </returns>
    public static bool RegisterTimerSkill(ITimerSkill skill)
    {
        ActiveTimerSkills.Add(skill);
        Type type = skill.GetType();
        if (type.IsSubclassOf(typeof(Skill)))
        {
            var skilled = (Skill)skill;
            TriggerAnim(skilled.ID, skill.TimeTaken);
        }
        return true;
    }

    /// <summary>
    /// Removes the provided skill from the Update list.
    /// </summary>
    /// <param name="skill"> The TimerSkill to be removed.  This function is currently NOT THREAD SAFE.  Only do it from the SkillUpdate() function provided by ITimerSkill </param>
    /// <returns> The success state of the function, always true for now. </returns>
    public static bool DeregisterTimerSkill(ITimerSkill skill)
    {
        ActiveTimerSkills.Remove(skill);
        return true;
    }

    /// <summary>
    /// The function that runs over all memebers of the TimerSkills list and triggers their SkillUpdate() function.
    /// This is NOT THREAD SAFE.  It will need to become so soon.
    /// </summary>
    private static void RunTimerSkills()
    {
        float time = Time.deltaTime;
        ActiveTimerSkills.ForEach(i => i.SkillUpdate(time));
    }

    /// <summary>
    /// A hook for skills to access Unity's Debug.Log() function
    /// </summary>
    /// <param name="log"> The message to be sent to Unity's Debug.Log() </param>
    public static void Log(string log)
    {
        Debug.Log(log);
    }

    /// <summary>
    /// Adds an AnimTriggerCallback object to the list as a triggerable animation
    /// </summary>
    /// <param name="callback"> The callback associated with the animation. </param>
    public static void RegisterAnim(AnimTriggerCallback callback)
    {
        AnimTriggers.Add(callback);
    }

    /// <summary>
    /// Triggers an animation based on the skill ID, and can modify the playtime of the animation based on the given time.
    /// </summary>
    /// <param name="skillID"> The skill's animation to be played. </param>
    /// <param name="time"> The duration of the skill.  This may be ignored, or left behind in some cases. </param>
    public static void TriggerAnim(SkillEnums.Skill skillID, float time)
    {
        int i = AnimTriggers.FindIndex(x => x.TriggerSkill == skillID);
        if (i >= 0)
        {
            AnimTriggers[i].Anim.TriggerAnim(time);
        }
    }

    /// <summary>
    /// Attatches the given display to the given skill, if possible.
    /// If not possible, it will log the fail event, and move on.
    /// </summary>
    /// <param name="skill"> The ID of the skill that will receive the display. </param>
    /// <param name="display"> The display that will connect to the skill. </param>
    public static void AttatchProgressDisplay(SkillEnums.Skill skill, IGeneralizedProgressDisplay display)
    {
        try
        {
            ITimerSkill Skill = (ITimerSkill)SkillList.Find(x => x.ID == skill);
            Skill.RegisterDisplay(display);
        }
        catch
        {
            Debug.Log("ITimerSkill cast failed in AttatchProgressDisplay");
        }
    }

    /// <summary>
    /// Attatches the provided display to a list for handling updates to an associated skill.
    /// </summary>
    /// <param name="display"> The display to track for updates. </param>
    public static void AttatchTextDisplay(IGeneralizedTextDisplay display) 
    {
        // Nasty block for making sure a Skill isn't selected as Slag without a SlagType.
        try 
        {
            if (((IStatTextDisplay)display).Stat == StatEnums.Slag) 
            {
                try 
                {
                    // Will error if it isn't slag type.  It should right now tho
                    if (((ISlagTextDisplay)display).SlagType >= SlagTypes.InferiorSlag)
                    {
                        // Slag has a type
                        TextDisplays.Add(display);
                        // This block is always hit, as all other slag types are higher in Enum value.
                    }
                }
                catch
                {
                    Debug.Log("Slag selected as a stat!  Not good choice, as it doesn't have a slag type!");
                }
            }
            else 
            {
                TextDisplays.Add(display);
            }
        }
        catch 
        {
            TextDisplays.Add(display);
        }
    }

    /// <summary>
    /// Buttons call this function to get their events to handle correctly for each skill
    /// </summary>
    /// <param name="SkillID"> The Enumerated ID of the skill the event will trigger on. </param>
    /// <param name="EventType">The Enumerated ID of the type of event that will trigger. </param>
    public static void ButtonEvent(SkillEnums.Skill SkillID, SkillEnums.ButtonEvent EventType)
    {
        // Filter by event type
        switch (EventType)
        {
            case SkillEnums.ButtonEvent.Level:
                try // Try to do the event anyways
                {
                    // There should only be one copy of the skill, so this shouldn't error.  Please replace with:
                    // List<Skill> skill = SkillList.FindAll(x => x.ID == SkillID);
                    // If there is an error.
                    ILevelable skill = (ILevelable)SkillList.Find(x => x.ID == SkillID);

                    // This will error (due to skill.LevelCost) if the skill isn't a levelable skill
                    // This will also return false if the player doesn't have enough SystemPoints to spend for the event.
                    if (SystemPointsCount.Sub(skill.LevelCost))
                    {
                        if (skill.Level == skill.MaxLevel)
                        {
                            skill.RankUp();
                        }
                        else
                        {
                            skill.LevelUp();
                        }
                    }
                }
                catch // Catch if it fails
                {
                    Debug.LogError("Tried to do a Level event on " + SkillID + " but it is not compatible or does not exist");
                }
                break;
            case SkillEnums.ButtonEvent.Activate:
                try
                {
                    // Find the skill and pretend that it is Activatable.
                    IActivatable skill = (IActivatable)SkillList.Find(x => x.ID == SkillID);

                    // Activate the skill.  This will throw an error if the skill wasn't implementing IActivatable.
                    skill.Activate();

                    // I should be done here.
                }
                catch // Catch if it fails
                {
                    Debug.LogError("Tried to do a Activate event on " + SkillID + " but it is not compatible or does not exist");
                }
                break;
            case SkillEnums.ButtonEvent.Toggle:
                try // Try to do the event anyways
                {
                    throw new NotImplementedException();
                }
                catch // Catch if it fails
                {
                    Debug.LogError("Tried to do a Toggle event on " + SkillID + " but it is not compatible or does not exist");
                }
                break;
            default:
                Debug.Log("ButtonEvent not of a defined EventID. MUST FIX.");
                break;
        }
    }

    /// <summary>
    /// Registers a Skill to the SkillList, so that it can be called by button events.
    /// </summary>
    /// <param name="skill"> The Skill that is being added. </param>
    public static void RegisterSkill(Skill skill)
    {
        if (SkillList.Exists(x => x.ID == skill.ID))
        {
            // It EXISTS
            Debug.LogError("Duplicate Skill found for Skill ID: " + skill.ID + " \n Please figure out why.");
        }
        else
        {
            // If it doesn't exist:
            SkillList.Add(skill);
        }
    }

    /// <summary>
    /// Used to update the text of any SkillTextDisplays.
    /// </summary>
    /// <param name="skillID"> The skill that is updated </param>
    /// <param name="dataType"> The type of data being updated. </param>
    /// <param name="text"> The new text to display. </param>
    public static void UpdateTextDisplay(SkillEnums.Skill skillID, DisplayEnums.TextDisplayType dataType ,string text) 
    {
        TextDisplays.FindAll(delegate(IGeneralizedTextDisplay x) 
        {
            try
            {
                if (((ISkillTextDisplay)x).Skill == skillID && ((ISkillTextDisplay)x).DisplayType == dataType)
                {
                    return true;
                }
                else 
                {
                    return false;
                }
            }
            catch 
            {
                return false;
            }
        }).ForEach(x => x.SetText(text));
    }

    /// <summary>
    /// Used to update the text of any SlagTextDisplays.
    /// </summary>
    /// <param name="slagType"> The Type of Slag being updated. </param>
    /// <param name="text"> The new text to display in the Text Display. </param>
    public static void UpdateTextDisplay(SlagTypes slagType, string text) 
    {
        TextDisplays.FindAll(delegate (IGeneralizedTextDisplay x)
        {
            try
            {
                if (((ISlagTextDisplay)x).SlagType == slagType)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }).ForEach(x => x.SetText(text));
    }

    /// <summary>
    /// Used to update the text of any StatTextDisplays.
    /// </summary>
    /// <param name="stat"> The stat being updated. </param>
    /// <param name="text"> The new text for the StatDisplay </param>
    public static void UpdateTextDisplay(StatEnums stat, string text) 
    {
        TextDisplays.FindAll(delegate (IGeneralizedTextDisplay x)
        {
            try
            {
                if (((IStatTextDisplay)x).Stat == stat)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }).ForEach(x => x.SetText(text));
    }

    /// <summary>
    /// Generates a JSON-formatted string by concatenating all skill save methods in a single Skills container.
    /// </summary>
    /// <returns> A JSON-formatted string containing the data of all registered Skills. </returns>
    public static string SerializeSkills()
    {
        string ret = "";

        SkillList.ForEach(x =>
        {
            // Debug.Log("Found Skill: " + x.ID);
            ret += ",{" + x.Save() + "}";
        });

        ret = ret[1..];

        return "[" + ret + "]";
    }

    /// <summary>
    /// Returns the requested Skill as a Skill object.
    /// Please remember to cast your object to your required state in order to interact properly.
    /// </summary>
    /// <param name="skillID"></param>
    /// <returns></returns>
    public static Skill GetSkill(SkillEnums.Skill skillID) 
    {
        return SkillList.Find(x => x.ID == skillID);
    }
    // END STATIC SECTION
}

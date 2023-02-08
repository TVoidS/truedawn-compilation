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
        ActiveTimerSkills.ForEach(i => i.SkillUpdate());
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

    //
    //  Start code for Button Event Handling:
    //

    /// <summary>
    /// Buttons call this function to get their events to handle correctly for each skill
    /// </summary>
    /// <param name="SkillID"> The Enumerated ID of the skill the event will trigger on. </param>
    /// <param name="EventType">The Enumerated ID of the type of event that will trigger. </param>
    public static void ButtonEvent(SkillEnums.Skill SkillID, SkillEnums.ButtonEvent EventType) 
    {
        //TODO:
        // 1. Find Skill By ID
        switch (EventType) 
        {
            case SkillEnums.ButtonEvent.Level:
                // TODO:
                // 2. Check if Skill is of appropriate type
                // 3. Trigger or Don't trigger event based on result from 2.
                // 4. If no trigger, log error.
                try // Try to do the event anyways
                {
                    // There should only be one copy of the skill, so this shouldn't error.  Please replace with:
                    // List<Skill> skill = SkillList.FindAll(x => x.ID == SkillID);
                    // If there is an error.
                    QiRegen skill = (QiRegen) SkillList.Find(x => x.ID == SkillID);

                    skill.LevelableCheck(); // This will error if there is no LevelableCheck() function predefined in the class.  At least it should.

                    Debug.Log("Level Event");
                }
                catch // Catch if it fails
                {
                    Debug.LogError("Tried to do a Level event on " + SkillID + " but it is not compatible or does not exist");
                }
                break;
            case SkillEnums.ButtonEvent.Activate:
                // TODO:
                // 2. Check if Skill is of appropriate type
                // 3. Trigger or Don't trigger event based on result from 2.
                // 4. If no trigger, log error.
                try // Try to do the event anyways
                {
                    
                }
                catch // Catch if it fails
                {
                    Debug.LogError("Tried to do a Activate event on " + SkillID + " but it is not compatible or does not exist");
                }
                break;
            case SkillEnums.ButtonEvent.Toggle:
                // TODO:
                // 2. Check if Skill is of appropriate type
                // 3. Trigger or Don't trigger event based on result from 2.
                // 4. If no trigger, log error.
                try // Try to do the event anyways
                {

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

    // END STATIC SECTION
}

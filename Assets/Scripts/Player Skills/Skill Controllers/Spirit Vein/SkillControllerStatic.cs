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
    private static readonly List<ITimerSkill> TimerSkills = new();

    private static readonly List<AnimTriggerCallback> AnimTriggers = new();

    /// <summary>
    /// Adds the skill to the list that will be ran over every frame by Unity.
    /// </summary>
    /// <param name="skill"> The skill to be Updated every frame. This should usually be "this" but can be other skills if a skill triggers multiple things. </param>
    /// <returns> Success state, true or false, always true for now. </returns>
    public static bool RegisterTimerSkill(ITimerSkill skill)
    {
        TimerSkills.Add(skill);
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
        Debug.Log(TimerSkills.ToString());
        TimerSkills.Remove(skill);
        Debug.Log(TimerSkills.ToString());
        return true;
    }

    /// <summary>
    /// The function that runs over all memebers of the TimerSkills list and triggers their SkillUpdate() function.
    /// This is NOT THREAD SAFE.  It will need to become so soon.
    /// </summary>
    private static void RunTimerSkills()
    {
        TimerSkills.ForEach(i => i.SkillUpdate());
    }

    /// <summary>
    /// A hook for skills to access Unity's Debug.Log() function
    /// </summary>
    /// <param name="log"> The message to be sent to Unity's Debug.Log() </param>
    public static void Log(string log)
    {
        Debug.Log(log);
    }

    public static void RegisterAnim(AnimTriggerCallback callback)
    {
        AnimTriggers.Add(callback);
    }

    public static void TriggerAnim(SkillEnums.Skill skillID, float time) 
    {
        int i = AnimTriggers.FindIndex(x => x.TriggerSkill == skillID);
        if (i >= 0)
        {
            AnimTriggers[i].Anim.TriggerAnim(time);
        }
    }

    // END STATIC SECTION
}

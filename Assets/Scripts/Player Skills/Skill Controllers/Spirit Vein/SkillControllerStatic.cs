using System.Collections.Generic;
using UnityEngine;

public partial class SkillController : MonoBehaviour
{
    // STATIC SECTION:

    // TODO: Make this thread-safe!
    // The exception relating to the RunTimerSkills() method is due to it deleting itself while it is looking at itself!

    private static readonly List<ITimerSkill> TimerSkills = new();

    public static bool RegisterTimerSkill(ITimerSkill skill)
    {
        TimerSkills.Add(skill);
        return true;
    }

    public static bool DeregisterTimerSkill(ITimerSkill skill)
    {
        Debug.Log(TimerSkills.ToString());
        TimerSkills.Remove(skill);
        Debug.Log(TimerSkills.ToString());
        return true;
    }

    private static void RunTimerSkills()
    {
        TimerSkills.ForEach(i => i.SkillUpdate());
    }

    public static void Log(string log)
    {
        Debug.Log(log);
    }

    // END STATIC SECTION
}

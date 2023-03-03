public interface ITimerSkill
{
    /// <summary>
    /// This is the function that all Timed skills will use to maintain their timers and update their displays.
    /// </summary>
    /// <param name="deltaTime"> The Time.deltaTime.  This is to avoid using UnityEngine everywhere when I can just send it.</param>
    public void SkillUpdate(float deltaTime);

    /// <summary>
    /// The time taken by the skill to achieve the desired effect.  Also applies to animation length.
    /// </summary>
    public float TimeTaken { get; }

    /// <summary>
    /// Will register a class that implements the IGeneralizedProgressDisplay interface
    /// for updating the value.
    /// This will allow the skill to be displayed in a in-world manner.
    /// </summary>
    /// <param name="display"> The display that will be registerd as valid. </param>
    public void RegisterDisplay(IGeneralizedProgressDisplay display);

    /// <summary>
    /// The flag on whether the TimerSkill should be processed.
    /// </summary>
    public bool IsActive { get; }
}

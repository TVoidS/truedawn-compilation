public interface ITimerSkill
{
    /// <summary>
    /// This is the function that all Timed skills will use to maintain their timers and update their displays.
    /// </summary>
    public void SkillUpdate();

    /// <summary>
    /// The time taken by the skill to achieve the desired effect.  Also applies to animation length.
    /// </summary>
    public float TimeTaken { get; }
}

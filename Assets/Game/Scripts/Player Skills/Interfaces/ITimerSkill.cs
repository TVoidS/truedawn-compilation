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
}

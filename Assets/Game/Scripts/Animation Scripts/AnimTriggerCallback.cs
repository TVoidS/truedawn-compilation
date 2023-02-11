public class AnimTriggerCallback
{
    /// <summary>
    /// The AnimationTrigger class that holds all the information regarding the animation
    /// </summary>
    public AnimationTrigger Anim { get; }

    /// <summary>
    /// The ID of the skill that triggers the animation.
    /// </summary>
    public SkillEnums.Skill TriggerSkill { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="anim"> The AnimationTrigger that holds all the information regarding the animation </param>
    /// <param name="trigger"> The ID of the skill that triggers the animation. </param>
    public AnimTriggerCallback(AnimationTrigger anim, SkillEnums.Skill trigger)
    {
        Anim = anim;
        TriggerSkill = trigger;
    }
}

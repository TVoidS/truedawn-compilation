public static class SkillEnums
{
    /// <summary>
    /// The approximate rate of stat growth to expect from the skill associated skill.
    /// </summary>
    public enum GrowthType 
    {
        Linear,
        Multiplicative,
        Exponential,
        Factorial,
        Mixed
    }

    /// <summary>
    /// The type of duration and activation expected from the associated skill.
    /// Likely to be replaced by Interfaces.
    /// </summary>
    public enum DurationType
    {
        Instant,
        Toggle,
        Passive,
        PassiveTimer,
        DelayedInstant
    }

    /// <summary>
    /// The ID enumerator for each skill.  This will eventually allow registering new skills from code.
    /// May take some time, as I would have to modify the SkillButton.cs to allow for it to be registered pre-load as well, and settable from the editor.
    /// </summary>
    public enum Skill 
    {
        QiRegen,
        QiConvert,
        QiPurity
    }

    /// <summary>
    /// The EventID for buttons.  This is here to help with Editor definitions.
    /// </summary>
    public enum ButtonEvent 
    {
        Level,
        Activate,
        Toggle
    }
}

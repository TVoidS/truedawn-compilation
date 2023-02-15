
/// <summary>
/// An alteration of the IGeneralizedTextDisplay for the purpose of connecting to Skills.
/// </summary>
public interface ISkillTextDisplay : IGeneralizedTextDisplay
{
    /// <summary>
    /// The skill that the text display is associated with.
    /// </summary>
    public SkillEnums.Skill Skill { get; }
}

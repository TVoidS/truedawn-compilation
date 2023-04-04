using static SkillEnums;

public interface ILevelable
{
    /// <summary>
    /// The level of a particular ILevelable Skill. It will have different effects per skill
    /// </summary>
    public byte Level { get; }

    /// <summary>
    /// The Max Level a particular ILevelable Skill.  It can be different for each Skill, but is often 9.
    /// </summary>
    public byte MaxLevel { get; }

    /// <summary>
    /// The Rank of a particular ILevelable Skill. It will have different effects per skill.
    /// </summary>
    public byte Rank { get; }

    /// <summary>
    /// The cost of going up one level, whatever that entails.
    /// </summary>
    public double LevelCost { get; }

    public static GrowthType Growth { get; }

    /// <summary>
    /// This will try to increment the Level of any ILevelable skill.  No return on failure yet.  Will later probably.
    /// </summary>
    public bool LevelUp();

    /// <summary>
    /// Run this whenever a potential change to level up costs occurs
    /// </summary>
    public void CalculateLevelCosts();
}

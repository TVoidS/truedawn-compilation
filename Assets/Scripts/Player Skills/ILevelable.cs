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
    /// This will try to increment the Level of any ILevelable skill.  No return on failure yet.  Will later probably.
    /// </summary>
    public void LevelUp();

    /// <summary>
    /// The setup function for buttons or other triggers that will connect to the LevelUp skill.
    /// It is NOT public, as it should only be accessed from within the class.
    /// It isn't private, because I'm not allowed to set it as such.
    /// </summary>
    void LevelUpSetup();
}

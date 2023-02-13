using UnityEngine;
using static SkillEnums;
using static UnityEngine.UI.Button;

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

    public ulong LevelCost { get; }

    public static GrowthType Growth { get; }

    /// <summary>
    /// This will try to increment the Level of any ILevelable skill.  No return on failure yet.  Will later probably.
    /// </summary>
    public void LevelUp();

    /// <summary>
    /// TODO: Determine if this needs to be public
    /// This method is meant to handle the event of leveling up when at max level.
    /// Increments the Rank, and should trigger an event on the skill ID for anything that may unlock on Rank Up.
    /// </summary>
    public void RankUp();

    /// <summary>
    /// Run this whenever a potential change to level up costs occurs
    /// </summary>
    public void CalculateLevelCosts();

    /// <summary>
    /// This is to verify that the class is actually ILevelable.
    /// </summary>
    /// <returns></returns>
    public void LevelableCheck();
}

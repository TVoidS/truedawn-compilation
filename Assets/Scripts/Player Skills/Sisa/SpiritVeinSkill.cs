using static SkillEnums;

public class SpiritVeinSkill : Skill
{
    public readonly byte Level;
    public readonly byte MaxLevel;
    public readonly byte Rank;
    public readonly GrowthType GrowthType;

    // Internally updateable, not externally
    protected ulong _UpgradeCost;
    // Allows outsiders to read internally updateable data.
    public ulong UpgradeCost => _UpgradeCost;

    public SpiritVeinSkill(byte id, DurationType duration,string name, string description, byte level, byte maxlevel, byte rank, GrowthType growth) : base(id, duration, name, description) 
    {
        Level = level;
        MaxLevel = maxlevel;
        Rank = rank;
        GrowthType = growth;

        CalculateUpgradeCost();
    }


    public void CalculateUpgradeCost() 
    {
        // TODO: Make this based on Level and Rank.
        _UpgradeCost = 5;
    }

    public bool RankUp() 
    {
        // This is the parent function, it should not be called to rank up the child!
        return false;
    }

    /// <summary>
    /// Tells the skill to attempt to level up.
    /// This should check for the requisite SP and update the PLayerStats and displays as needed.
    /// </summary>
    /// <returns> Success Status </returns>
    public bool LevelUp() 
    {
        // This is the parent function, it should not be called to rank up the child!
        return false;
    }
}

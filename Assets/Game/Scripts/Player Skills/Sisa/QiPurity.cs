public class QiPurity : SpiritVeinSkill, ILevelable
{
    private uint purity;

    public QiPurity(byte level, byte rank) : 
        base
        (
            SkillEnums.Skill.QiPurity,
            SkillEnums.DurationType.Passive,
            "Qi Purity",
            "This Skill represents the Purity of your Qi. The higher your purity, the more slag you get from conversion, and a few other bonuses besides."
        ) 
    {

        _Level = level;
        _Rank = rank;

        // Set the level costs
        CalculateLevelCosts();

        // Set the current purity for the purposes of QiConversion and any other skill that requires it.
        LoadPurity();

        // Add the skill to the SkillController SkillList for saving and handling events.
        SkillController.RegisterSkill(this);
    }

    private void LoadPurity() 
    {
        purity = (uint)(_Level + 1);
    }

    public uint GetPurity() { return purity; }

    // ILevelable Interface Implementations
    private byte _Level;
    public byte Level => _Level;

    public byte MaxLevel => 9;

    private byte _Rank;
    public byte Rank => _Rank;

    private ulong _LevelCost;
    public ulong LevelCost => _LevelCost;

    public void CalculateLevelCosts()
    {
        // TODO: Make this scale based on level.
        _LevelCost = 1;
    }

    public void LevelUp()
    {
        _Level++;
    }

    public void RankUp()
    {
        _Level = 0;
        _Rank++;
    }

    public override string Save()
    {
        // Get the data shared by all skills, then add this skill's data, as needed by any particular interface.
        var ret = base.Save() + ",\"Level\":"+_Level+",\"Rank\":"+_Rank;
        return ret;
    }
}

public class QiPurity : SpiritVeinSkill, ILevelable
{
    private uint purity;

    public QiPurity() : 
        base
        (
            SkillEnums.Skill.QiPurity,
            SkillEnums.DurationType.Passive,
            "Qi Purity",
            "This Skill represents the Purity of your Qi. The higher your purity, the more slag you get from conversion, and a few other bonuses besides."
        ) 
    {
        LoadPurity();
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
        throw new System.NotImplementedException();
    }

    public void LevelableCheck()
    {
        // Leave Empty.
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
}

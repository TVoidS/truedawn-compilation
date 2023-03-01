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

        // Add the skill to the SkillController SkillList for saving and handling events.
        SkillController.RegisterSkill(this);

        // Set the current purity for the purposes of QiConversion and any other skill that requires it.
        LoadPurity();

        // Set the level costs
        CalculateLevelCosts();

        // Set all other displays 
        UpdateLevelDisplays();
        base.UpdateAllText();
        UpdateFancyRankDisplays();
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

    private double _LevelCost;
    public double LevelCost => _LevelCost;

    public void CalculateLevelCosts()
    {
        _LevelCost = LevelCosts.CalculateCost(_Level, MaxLevel, _Rank, 10);
        UpdateLevelCostDisplays();
    }

    public void LevelUp()
    { 
        // If it leveled,
        if (_Level == MaxLevel)
        {
            // And we are at max level,
            _Level = 0;
            _Rank++;
            // Update the Rank Displays
            UpdateFancyRankDisplays();
        }
        else
        {
            _Level++;
        }

        // Recalculate the Level Costs
        CalculateLevelCosts();
        // And update the displays.
        UpdateLevelDisplays();
    }

    public void RankUp()
    {
        _Level = 0;
        _Rank++;
    }

    // Skill Override section
    public override string Save()
    {
        // Get the data shared by all skills, then add this skill's data, as needed by any particular interface.
        var ret = base.Save() + ",\"Level\":"+_Level+",\"Rank\":"+_Rank;
        return ret;
    }
    public override void UpdateAllText()
    {
        base.UpdateAllText();
        UpdateLevelDisplays();
        UpdateFancyRankDisplays();
        UpdateLevelCostDisplays();
    }

    // Display code for internal use:
    private void UpdateLevelDisplays()
    {
        SkillController.UpdateTextDisplay(ID, DisplayEnums.TextDisplayType.Level, "T" + _Rank + "G" + _Level);
    }

    private void UpdateFancyRankDisplays()
    {
        SkillController.UpdateTextDisplay(ID, DisplayEnums.TextDisplayType.RankFancy, "NOT IMPLEMENTED"); // TODO
    }

    private void UpdateLevelCostDisplays()
    {
        SkillController.UpdateTextDisplay(ID, DisplayEnums.TextDisplayType.LevelCost, _LevelCost + " SP"); // TODO Make this not big number only.
        // Do shorthand or something.  Like 43M or 1B or 203Sp.
    }
}

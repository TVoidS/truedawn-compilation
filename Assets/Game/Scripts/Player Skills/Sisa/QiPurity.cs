using System.Text.Json;

public class QiPurity : SpiritVeinSkill, ILevelable
{
    private ushort purity;

    public QiPurity() : 
        base
        (
            SkillEnums.Skill.QiPurity,
            SkillEnums.DurationType.Passive,
            "Qi Purity",
            "This Skill represents the Purity of your Qi. The higher your purity, the more slag you get from conversion, and a few other bonuses besides."
        ) 
    {

        _Level = 0;
        _Rank = 0;

        // Add the skill to the SkillController SkillList for saving and handling events.
        SkillController.RegisterSkill(this);

        // Set the current purity for the purposes of QiConversion and any other skill that requires it.
        LoadPurity();
    }

    /// <summary>
    /// Recalculates the Purity for external viewing.
    /// This occurs after each LevelUp command.
    /// </summary>
    private void LoadPurity() 
    {
        purity = (ushort)((_Rank * 10) + _Level);
    }

    /// <summary>
    /// The Current Purity value without the Level/Rank separate values.
    /// This starts at 0 on a new game and goes up by one each time a successful level up occurs.
    /// </summary>
    /// <returns> The Purity value. </returns>
    public ushort GetPurity() { return purity; }

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

    public bool LevelUp()
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

        LoadPurity();

        // Recalculate the Level Costs
        CalculateLevelCosts();
        // And update the displays.
        UpdateLevelDisplays();

        // NOTE: Change this to a more targeted system 
        // ((QiConvert)SkillController.GetSkill(SkillEnums.Skill.QiConvert)).SetAllGains();

        // Modify this if there is ever a cause for failure.
        return true;
    }

    // Skill Override section
    public override string Save(byte tabcount)
    {
        string tabs = "";
        for (byte i = 0; i < tabcount; i++)
        {
            tabs += "\t";
        }

        return tabs + "{\n"
            + tabs + "\t\"ID\":\"" + ID + "\",\n"
            + tabs + "\t\"Level\":" + Level + ",\n"
            + tabs + "\t\"Rank\":" + Rank + "\n"
            + tabs + "}";
    }
    public override void UpdateAllText()
    {
        UpdateLevelDisplays();
        UpdateFancyRankDisplays();
        UpdateLevelCostDisplays();
    }

    public override void Load(JsonElement skillData)
    {
        // No real need for this line
        base.Load(skillData);

        _Level = skillData.GetProperty("Level").GetByte();
        _Rank = skillData.GetProperty("Rank").GetByte();

        // Recalculate things due to loaded level and rank. 
        CalculateLevelCosts();

        LoadPurity();

        UpdateAllText();
    }

    public override void Startup()
    {
        base.Startup();

        // Set the level costs
        CalculateLevelCosts();

        // Set all other displays 
        UpdateAllText();
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

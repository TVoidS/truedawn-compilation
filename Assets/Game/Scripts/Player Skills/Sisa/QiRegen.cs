using System.Collections.Generic;
using System.Text.Json;
using static SkillEnums;

public class QiRegen : SpiritVeinSkill, ITimerSkill, ILevelable
{
    private uint RegenQuantity = 1;

    // Represents the number of seconds to regen Qi
    private float _timeTaken = 6f;
    public float TimeTaken => _timeTaken;

    private float Progress;

    /// <summary>
    /// Initiates the QiRegen skill for the player.
    /// </summary>
    public QiRegen() : 
        // Everything fed into the base() call is generic data that all skills have, but customized to Qi Regen.
        base(SkillEnums.Skill.QiRegen,
             DurationType.PassiveTimer,
             "Qi Regeneration",
             "The basic ability all Spirit Veins have.  To recover Qi naturally. \n You however have the ability to recover Qi much faster. \n If you invest in this Skill that is...")
    {
        // Recieved and Standard values
        _Level = 0;
        _Rank = 0;
        Progress = 0f;

        RegenQuantity = (uint) (_Level + (10 * _Rank) + 1);

        SkillController.RegisterTimerSkill(this);
        SkillController.RegisterSkill(this);

        isActive = false;
    }

    private void CalculateRegenQuantity() 
    {
        RegenQuantity = (uint)(_Level + (10 * _Rank) + 1);
    }

    // Interface ITimerSkill implementation
    public void SkillUpdate(float deltaTime)
    {
        Progress += (deltaTime / _timeTaken);
        if (Progress >= 1f)
        {
            PlayerEventCount.RegisterEvent("qiregen.regenqi:Cycles", 1);
            Progress = 0f;
            QiCount.Add(RegenQuantity);
        }
        UpdateDisplays(Progress);
    }

    /// <summary>
    /// Internal storage for the flag.
    /// This allows us to modify it with local code.
    /// </summary>
    private bool isActive = false;

    /// <summary>
    /// External view for whether the skill is active.
    /// Determines whether the SkillUpdate function runs.
    /// </summary>
    public bool IsActive => isActive;

    /// <summary>
    /// Used to handle display of any and all progress bars.
    /// </summary>
    private readonly List<IGeneralizedProgressDisplay> _displays = new();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="display"></param>
    public void RegisterDisplay(IGeneralizedProgressDisplay display) 
    {
        _displays.Add(display);
    }

    private void UpdateDisplays(float newValue) 
    {
        _displays.ForEach(x => 
        {
            x.UpdateValue(newValue);
        });
    }

    // Interface ILevelable Implementation
    private byte _Level;
    public byte Level => _Level;

    public byte MaxLevel => 9;

    private byte _Rank;
    public byte Rank => _Rank;

    private double _LevelCost;
    public double LevelCost => _LevelCost;

    public readonly GrowthType Growth = GrowthType.Linear;

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

        RegenQuantity = (uint)(_Level + (10 * _Rank) + 1);

        // Recalculate the Level Costs
        CalculateLevelCosts();
        // And update the displays.
        UpdateLevelDisplays();

        // Change this if there is ever a problem that can cause a failure of leveling.
        return true;
    }

    public void CalculateLevelCosts() 
    {
        _LevelCost = LevelCosts.CalculateCost(_Level, _Rank, 10);
        UpdateLevelCostDisplays();
    }
    // End Interface Implementations

    // Skill Override Section
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
        base.UpdateAllText();
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

        UpdateAllText();
    }

    public override void Startup()
    {
        base.Startup();

        isActive = true;

        CalculateLevelCosts();
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

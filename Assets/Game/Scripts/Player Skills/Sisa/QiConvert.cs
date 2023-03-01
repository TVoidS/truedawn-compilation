using System.Collections.Generic;
using static SkillEnums;

public class QiConvert : SpiritVeinSkill, ITimerSkill, ILevelable, IActivatable
{
    /// <summary>
    /// Construct the Qi Conversion skill.
    /// This should only be run ONCE.  And even then, only from the SkillController.
    /// </summary>
    /// <param name="level"> The player's level in the skill </param>
    /// <param name="rank"> The player's rank in the skill </param>
    public QiConvert(byte level, byte rank) :
        base(SkillEnums.Skill.QiConvert,
             DurationType.DelayedInstant, // Skill's trigger/duration type, tells how to treat the trigger event Probably useless, but keeping for now.
             "Qi Conversion",
             "This skill converts your Qi into Spirit Slag of various types!")
    {
        // Loaded and Standard Data
        _Level = level;
        _Rank = rank;

        // Just booted up, there is no progress towards the next Conversion
        Progress = 0f;

        // Add the skill to the SkillList list for event tracking, saving, and identification.
        SkillController.RegisterSkill(this);

        // Set the cost of leveling.
        CalculateLevelCosts();
        CalculateTime();

        // Set all other displays 
        UpdateLevelDisplays();
        base.UpdateAllText();
        UpdateFancyRankDisplays();
    }

    /// <summary>
    /// The status of the conversion process.
    /// True if the progress bar is filling up.
    /// </summary>
    private bool Converting = false;

    // Interface Implementation from IActivatable.
    public void Activate() 
    {
        // TODO: register itself to the passive list 
        if (Converting)
        {
            // fail.
            SkillController.Log("BUSY.  Hold your horses!");
        }
        else if (QiCount.Sub(1))
        {
            // TODO: change the 1 in sub() to QiCost for dynamic cost caluclations
            // Start Converting!
            Converting = true;
            SkillController.RegisterTimerSkill(this);
        }
        else
        {
            // Too broke
            SkillController.Log("Not Enough Qi");
        }
    }

    // Variables used in SkillUpdate()
    private float Progress;

    // Interface Implementation from ITimerSkill
    public void SkillUpdate(float deltaTime)
    {
        // This just checks if the bar is done, then finishes the convert.
        // It then removes itself from the TimerSkills list in SkillController.  This may need to be Thread-Safe later...
        if (Progress >= 1f)
        {
            Progress = 0f;
            SlagCount.Add(gains, SlagTypes.InferiorSlag);
            Converting = false;
            SkillController.DeregisterTimerSkill(this);
        }
        else
        {
            Progress += (deltaTime / TimeTaken);
        }
        UpdateDisplays(Progress);
    }

    /// <summary>
    /// The list of progress bars that are going to be updated each frame.
    /// </summary>
    private readonly List<IGeneralizedProgressDisplay> displays = new();

    /// <summary>
    /// Adds the displays to a privatly stored list that contains progress bars.
    /// These progress bars will be updated frame by frame for display purposes.
    /// </summary>
    /// <param name="display"></param>
    public void RegisterDisplay(IGeneralizedProgressDisplay display) 
    {
        displays.Add(display);
    }

    /// <summary>
    /// Sends the update message to the attatched displays.
    /// </summary>
    /// <param name="newValue"> The new % of the displays. </param>
    private void UpdateDisplays(float newValue) 
    {
        displays.ForEach(x=> { x.UpdateValue(newValue); });
    }

    /// <summary>
    /// The mass of spirit slag of a given type gained from each conversion
    /// TODO: Make this be more than a single ulong.  One for each type of slag!
    /// </summary>
    private ulong gains = 10;

    /// <summary>
    /// Recalculates the Conversion Gains for each type of slag.
    /// This should be triggered whenever there is a change to the Qi Purity
    /// </summary>
    /// <returns> True if succeeded, false otherwise. </returns>
    public void CalculateGains()
    {
        // TODO: make this dependent on the type of slag being produced.
        gains = ((QiPurity)SkillController.GetSkill(SkillEnums.Skill.QiPurity)).GetPurity()*10;
    }

    /// <summary>
    /// The time taken by the system to complete the conversion of Qi to Slag.
    /// TODO: Make this have multiple floats.  One for each type of SLAG! 
    /// Or at least recalculate it every time it changes selection...
    /// </summary>
    public float TimeTaken => _timeTaken;
    private float _timeTaken = 8f;

    /// <summary>
    /// Recalculates the Time Requirements of each slag's conversion from Qi.
    /// This should be triggered each time there is a change in Level or Qi Purity!
    /// </summary>
    /// <returns> True if succeeded, false otherwise. </returns>
    public void CalculateTime()
    {
        // TODO: Make this based off the type of slag being produced.
        // Base time of 60 seconds for now.
        _timeTaken = 6f;
    }


    // TODO: Separate Implementations into a different Partial class file
    // ILevelable Interface Implementation:
    private byte _Level;
    public byte Level => _Level;

    private byte _Rank;
    public byte Rank => _Rank;

    public byte MaxLevel => 9;

    private double _LevelCost;
    public double LevelCost => _LevelCost;

    public GrowthType Growth = GrowthType.Linear;

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

        // Recalculate Time
        CalculateTime();
        // Recalculate the Level Costs
        CalculateLevelCosts();
        // And update the displays.
        UpdateLevelDisplays();
    }

    public void RankUp() 
    {
        //TODO:
        _Rank++;
        _Level = 0;
        CalculateLevelCosts();
        CalculateTime();
    }

    public void CalculateLevelCosts() 
    {
        _LevelCost = LevelCosts.CalculateCost(_Level, MaxLevel, _Rank, 10);
        UpdateLevelCostDisplays();
    }

    // Overrides of the Skill class
    public override string Save()
    {
        return base.Save() + ",\"Level\":" + Level + ",\"Rank\":" + Rank;
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

using NUnit.Framework;
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
             DurationType.DelayedInstant, // Skill's trigger/duration type, tells how to treat the trigger event
             "Qi Conversion",
             "This skill converts your Qi into Spirit Slag of various types!")
    {
        // Loaded and Standard Data
        _Level = level;
        _Rank = rank;
        _MaxLevel = 9;

        Value = 0f;

        // Add the skill to the SkillList list for event tracking and identification.
        SkillController.RegisterSkill(this);
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
    private float Value;

    // Interface Implementation from ITimerSkill
    public void SkillUpdate(float deltaTime)
    {
        // This just checks if the bar is done, then finishes the convert.
        // It then removes itself from the TimerSkills list in SkillController.  This may need to be Thread-Safe later...
        if (Value >= 1f)
        {
            Value = 0f;
            SlagCount.Add(gains, SlagCount.Type.InfereriorSpiritSlag);
            Converting = false;
            SkillController.DeregisterTimerSkill(this);
        }
        else
        {
            Value += (deltaTime / TimeTaken);
        }
        UpdateDisplays(Value);
    }

    private readonly List<IGeneralizedProgressDisplay> displays = new();

    public void RegisterDisplay(IGeneralizedProgressDisplay display) 
    {
        displays.Add(display);
    }

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
    public bool RecalculateGains()
    {
        // TODO: make this based off of the QiPurity skill level and rank.
        gains = 10;
        return true;
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
    public bool RecalculateTime()
    {
        // TODO: make this based off of the QiConversion skill level and rank
        _timeTaken = 8f;
        return true;
    }


    // TODO: Separate Implementations into a different Partial class file
    // ILevelable Interface Implementation:
    private byte _Level;
    public byte Level => _Level;

    private byte _Rank;
    public byte Rank => _Rank;

    private byte _MaxLevel;
    public byte MaxLevel => _MaxLevel;

    private ulong _LevelCost;
    public ulong LevelCost => _LevelCost;

    public GrowthType Growth = GrowthType.Linear;

    public void LevelUp() 
    {
        //TODO:
        _Level++;
    }

    public void RankUp() 
    {
        //TODO:
        _Rank++;
        _Level = 0;
    }

    public void CalculateLevelCosts() 
    {
        //TODO:
    }

    /// <summary>
    /// Just a method that only exists for Levelable skills.
    /// </summary>
    public void LevelableCheck() 
    {
        // Complete.  Please don't touch this.
    }
}

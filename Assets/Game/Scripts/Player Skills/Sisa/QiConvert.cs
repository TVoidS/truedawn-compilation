using System;
using System.Collections.Generic;
using System.Text.Json;
using static SkillEnums;

public class QiConvert : SpiritVeinSkill, ITimerSkill, ILevelable, IActivatable, ISkillEventResponder
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

        //SetAllGains();

        // Add the skill to the SkillList list for event tracking, saving, and identification.
        SkillController.RegisterSkill(this);
        SkillController.RegisterTimerSkill(this);

        // Set the cost of leveling.
        CalculateLevelCosts();
        CalculateTime();

        // Set all other displays 
        UpdateAllText();

        SkillController.RegisterSkillEventCallback(new SkillEventCallback(SkillEnums.Skill.QiPurity, this, SkillEnums.ButtonEvent.Level));
    }

    public void SetType(byte type) 
    {
        try
        {
            currType = (SlagTypes)type;
        }
        catch 
        {
            // NO CHANGE
            SkillController.Log("Invalid SlagType for Conversion");
        }
    }

    private uint qiCost = 1;

    private void setQiCost() 
    {
        // NOTE: Change this to a dynamic calculation if it ever changes how it is done.
        qiCost = 1;
    }

    // Interface Implementation from IActivatable.
    public bool Activate() 
    {
        if (isActive)
        {
            // fail.
            SkillController.Log("BUSY.  Hold your horses!");
            return false;
        }
        else if (QiCount.Sub(qiCost))
        {
            // Start Converting!
            isActive = true;
            CalculateTime();
            SkillController.TriggerAnim(ID, TimeTaken);
            return true;
        }
        else
        {
            // Too broke
            SkillController.Log("Not Enough Qi");
            return false;
        }
    }

    // Variables used in SkillUpdate()
    private float Progress;
    
    private SlagTypes currType = SlagTypes.InferiorSlag;
    private SlagTypes nextType = SlagTypes.InferiorSlag;

    // Interface Implementation from ITimerSkill
    public void SkillUpdate(float deltaTime)
    {
        // This just checks if the bar is done, then finishes the convert.
        // It then removes itself from the TimerSkills list in SkillController.  This may need to be Thread-Safe later...
        if (Progress >= 1f)
        {
            Progress = 0f;
            SlagCount.Add(slagGains[currType], SlagTypes.InferiorSlag);
            // SkillController.DeregisterTimerSkill(this);
            isActive = false;
        }
        else
        {
            Progress += (deltaTime / TimeTaken);
        }
        UpdateDisplays(Progress);
    }

    public void SetNextType(string desc) 
    {
        foreach(SlagTypes type in Enum.GetValues(typeof(SlagTypes))) 
        {
            if (type.ToDiscriptionString().Equals(desc)) 
            {
                nextType = type;
                break;
            }
        }
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
        displays.ForEach(x => { x.UpdateValue(newValue); });
    }

    /// <summary>
    /// The mass of spirit slag of a given type gained from each conversion
    /// </summary>
    private Dictionary<SlagTypes, ulong> slagGains = new Dictionary<SlagTypes, ulong>();

    /// <summary>
    /// Sets all of the gains for the given slagtype.
    /// </summary>
    /// <param name="Type"> The slag type to generate the gains for. </param>
    private void SetGain(SlagTypes Type) 
    {
        slagGains[Type] = 0;

        int purity = ((QiPurity)SkillController.GetSkill(SkillEnums.Skill.QiPurity)).GetPurity();

        int pureCheck = purity - (10 * (int)Type);

        if (pureCheck >= 0)
        {
            int pureGrade = pureCheck % 10;
            pureCheck -= pureGrade;
            int pureTier = pureCheck / 10;

            slagGains[Type] = (ulong)(pureTier + 1) * (ulong)(pureGrade + 1) * 10; // This gives me the amount in the tens place (and the hundreds place, but that gets added to later)

            // We can now ignore pureGrade

            while (pureTier > 0) 
            {
                // Extremely simple loop.  Hopefully it doesn't cause problemts, but it shouldn't.
                slagGains[Type] += (ulong)pureTier * 100;

                pureTier -= 1;
            }

        }
        else if (pureCheck == -1)
        {
            slagGains[Type] = 1;
        }
        else 
        {
            slagGains[Type] = 0;
        }
    }

    /// <summary>
    /// Recalculates the Conversion Gains for each type of slag.
    /// This should be triggered whenever there is a change to the Qi Purity
    /// </summary>
    /// <returns> True if succeeded, false otherwise. </returns>
    public void SetAllGains() 
    {
        foreach (SlagTypes Type in Enum.GetValues(typeof(SlagTypes))) 
        {
            SetGain(Type);
        }
    }

    /// <summary>
    /// The time taken by the system to complete the conversion of Qi to Slag.
    /// TODO: Make this have multiple floats.  One for each type of SLAG! 
    /// Or at least recalculate it every time it changes selection...
    /// </summary>
    public float TimeTaken => _timeTaken;
    private float _timeTaken = 6f;

    // TODO: Create the different time arrays.

    private float[] time1 = { 60, 55, 50, 45, 40, 35, 30, 25, 20, 15};
    private float[] time2 = { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1.5f };
    // Everything else is just +1 so far.

    /// <summary>
    /// Recalculates the Time Requirements of each slag's conversion from Qi.
    /// This should be triggered each time there is a change in Level or Qi Purity!
    /// </summary>
    /// <returns> True if succeeded, false otherwise. </returns>
    public void CalculateTime()
    {
        // TODO: Make this based off the type of slag being produced.
        // Base time of 60 seconds for now.
        int purity = ((QiPurity)SkillController.GetSkill(SkillEnums.Skill.QiPurity)).GetPurity();

        int pureCheck = purity - (10 * (int)currType);

        if (pureCheck < 0)
        {
            // FAIL UTTERLY.
            _timeTaken = float.MaxValue;
        }
        else if (pureCheck >= 20)
        {
            // +1's
            _timeTaken = 1 / (pureCheck - 20);
        }
        else if (pureCheck >= 10)
        {
            // time2
            pureCheck -= 10;
            _timeTaken = time2[pureCheck];
        }
        else 
        {
            // time 1
            _timeTaken = time1[pureCheck];
        }
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

        // Recalculate Time
        CalculateTime();
        // Recalculate the Level Costs
        CalculateLevelCosts();
        // And update the displays.
        UpdateLevelDisplays();

        // Modify this if there is ever a cause for failure other than cost.
        return true;
    }

    public void CalculateLevelCosts() 
    {
        _LevelCost = LevelCosts.CalculateCost(_Level, MaxLevel, _Rank, 10);
        UpdateLevelCostDisplays();
    }

    // Overrides of the Skill class
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
        CalculateTime();

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

    // ISkillEventResponder Interface implementation
    public void Trigger() 
    {
        SetAllGains();
    }
}

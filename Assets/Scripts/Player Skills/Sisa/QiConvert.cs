using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static SkillEnums;
using static UnityEngine.UI.Button;

public class QiConvert : SpiritVeinSkill, ITimerSkill, ILevelable, IActivatable
{
    /// <summary>
    /// The bar that shows how close to done we are
    /// </summary>
    private Slider progressBar;


    /// <summary>
    /// Construct the Qi Conversion skill.
    /// This should only be run ONCE.  And even then, only from the SkillController.
    /// </summary>
    /// <param name="id"> The Skill's unique ID </param>
    /// <param name="level"> The player's level in the skill </param>
    /// <param name="rank"> The player's rank in the skill </param>
    /// <param name="convertSlider"> The worldspace slider that represents the progress </param>
    /// <param name="convertSelector"> The worldspace dropdown that represents what material is to be generated </param>
    /// <param name="convertTrigger"> The worldspace button that triggers the conversion process to begin </param>
    /// <param name="levelTrigger"> The Worldspace button that triggers the LevelUp event. </param>
    public QiConvert(byte level, byte rank, Slider convertSlider) :
        base(SkillEnums.Skill.QiConvert,
             DurationType.DelayedInstant, // Skill's trigger/duration type, tells how to treat the trigger event
             "Qi Conversion",
             "This skill converts your Qi into Spirit Slag of various types!")
    {
        // Loaded and Standard Data
        _Level = level;
        _Rank = rank;
        _MaxLevel = 9;

        progressBar = convertSlider;

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
            Debug.Log("BUSY.  Hold your horses!");
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
            Debug.Log("Not Enough Qi");
        }
    }

    // Interface Implementation from ITimerSkill
    public void SkillUpdate()
    {

        // This just checks if the bar is done, then finishes the convert.
        // It then removes itself from the TimerSkills list in SkillController.  This may need to be Thread-Safe later...
        if (progressBar.value >= 1)
        {
            progressBar.value = 0;
            SlagCount.Add(gains, SlagCount.Type.InfereriorSpiritSlag);
            Converting = false;
            SkillController.DeregisterTimerSkill(this);
        }
        else
        {
            progressBar.value += (Time.deltaTime / TimeTaken);
        }
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
    }

    public void RankUp() 
    {
        //TODO:
    }

    public void LevelUpSetup(ButtonClickedEvent UITrigger, string KeyTrigger) 
    {
        //TODO:
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

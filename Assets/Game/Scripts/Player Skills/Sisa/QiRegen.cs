using System.Collections.Generic;
using static SkillEnums;

public class QiRegen : SpiritVeinSkill, ITimerSkill, ILevelable
{
    // TODO: Tie this to Qi Purity
    private uint RegenQuantity = 1;

    // Represents the number of seconds to regen Qi
    private float _timeTaken = 6f;
    public float TimeTaken => _timeTaken;

    private float Progress;

    /// <summary>
    /// Initiates the QiRegen skill for the player.
    /// </summary>
    /// <param name="level"> The player's level in Qi Regen </param>
    /// <param name="rank"> The player's rank in Qi Regen </param>
    public QiRegen(byte level, byte rank) : 
        // Everything fed into the base() call is generic data that all skills have, but customized to Qi Regen.
        base(SkillEnums.Skill.QiRegen,
             DurationType.PassiveTimer,
             "Qi Regeneration",
             "The basic ability all Spirit Veins have.  To recover Qi naturally. \n You however have the ability to recover Qi much faster. \n If you invest in this Skill that is...")
    {
        // Recieved and Standard values
        _MaxLevel = 9;
        _Level = level;
        _Rank = rank;
        Progress = 0f;

        SkillController.RegisterTimerSkill(this);
        SkillController.RegisterSkill(this);
    }

    // Interface ITimerSkill implementation
    public void SkillUpdate(float deltaTime)
    {
        Progress += (deltaTime / _timeTaken);
        if (Progress >= 1f)
        {
            // TODO: Send Display Update event 
            Progress = 0f;
            QiCount.Add(RegenQuantity);
        }
        UpdateDisplays(Progress);
    }

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

    private byte _MaxLevel;
    public byte MaxLevel => _MaxLevel;

    private byte _Rank;
    public byte Rank => _Rank;

    private ulong _LevelCost;
    public ulong LevelCost => _LevelCost;

    public readonly GrowthType Growth = GrowthType.Linear;

    public void LevelUp() 
    {
        // TODO: +Complete
        _Level++;
    }

    public void RankUp() 
    {
        // TODO: Complete
        _Rank++;
        _Level = 0;
    }

    public void CalculateLevelCosts() 
    {
        // TODO: Everythign
    }

    public void LevelableCheck() 
    {
        // Doesn't need to do anything other than exist.
        
    }
    // End Interface Implementations
}

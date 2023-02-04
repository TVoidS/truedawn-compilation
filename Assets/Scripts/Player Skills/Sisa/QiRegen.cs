using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SkillEnums;
using static UnityEngine.UI.Button;

public class QiRegen : SpiritVeinSkill, ITimerSkill, ILevelable
{
    // TODO: Tie this to Qi Purity
    private uint RegenQuantity = 1;

    // Represents the number of seconds to regen Qi
    private float _timeTaken = 6f;
    public float TimeTaken => _timeTaken;

    private readonly Slider RegenDisplaySlider;

    public QiRegen(byte level, byte rank, Slider regen, ButtonClickedEvent LevelUITrigger, string LevelKeyTrigger) : 
        base(SkillEnums.Skill.QiRegen,
             DurationType.PassiveTimer,
             "Qi Regeneration",
             "The basic ability all Spirit Veins have.  To recover Qi naturally. \n You however have the ability to recover Qi much faster. \n If you invest in this Skill that is...")
    {
        // Recieved and Standard values
        _MaxLevel = 9;
        _Level = level;
        _Rank = rank;
        LevelUpSetup(LevelUITrigger, LevelKeyTrigger);


        RegenDisplaySlider = regen;
        RegenDisplaySlider.value = 0f;
        SkillController.RegisterTimerSkill(this);
    }

    // Interface ITimerSkill implementation
    public void SkillUpdate()
    {
        RegenDisplaySlider.value += (Time.deltaTime / _timeTaken);
        if (RegenDisplaySlider.value >= 1f)
        {
            RegenDisplaySlider.value = 0f;
            QiCount.Add(RegenQuantity);
        }
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
        // TODO: Implement
    }

    public void RankUp() 
    {
        // TODO:
    }

    public void LevelUpSetup(ButtonClickedEvent UITrigger, string KeyTrigger) 
    {
        // TODO:
    }

    public void CalculateLevelCosts() 
    {
        // TODO:
    }
    // End Interface Implementations
}

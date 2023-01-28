using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SkillEnums;

public class QiRegen : SpiritVeinSkill, ITimerSkill
{
    // TODO: Tie this to Qi Purity
    private uint RegenQuantity = 1;

    // Represents the number of seconds to regen Qi
    private float TickRegenCost = 6f;

    private readonly Slider RegenDisplaySlider;

    public QiRegen(byte id, byte level, byte rank, Slider regen) : 
        base(id,
             DurationType.PassiveTimer,
             "Qi Regeneration",
             "The basic ability all Spirit Veins have.  To recover Qi naturally. \n You however have the ability to recover Qi much faster. \n If you invest in this Skill that is...",
             level,
             9,
             rank,
             GrowthType.Linear)
    {
        RegenDisplaySlider = regen;
        RegenDisplaySlider.value = 0f;
        SkillController.RegisterTimerSkill(this);
    }

    public void SkillUpdate()
    {
        RegenDisplaySlider.value += (Time.deltaTime / TickRegenCost);
        if (RegenDisplaySlider.value >= 1f)
        {
            RegenDisplaySlider.value = 0f;
            QiCount.Add(RegenQuantity);
        }
    }
}

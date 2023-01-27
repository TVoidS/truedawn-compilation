using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SkillEnums;

public class QiRegen : SpiritVeinSkill
{
    private float _cooldown = 1.0f;
    public float cooldown => _cooldown;

    // TODO: Tie this to Qi Purity
    private uint regenQuantity = 1;

    // Represents the number of seconds to regen Qi
    private float tickRegenCost = 6f;

    private Slider _Regener;

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
        _Regener = regen.GetComponent<Slider>();
        _Regener.value = 0f;
    }

    public bool RankUp()
    {
        if (Level < MaxLevel)
        {
            return false;
        }
        else 
        {
            // TODO: Make more checks and take System Points to achieve.
            return true;
        }
    }

    public bool LevelUp() // TODO: Need to make this have checks for validity, and take System Points from the Player.
    {
        throw new System.NotImplementedException();
    }

    public bool LevelUpSetup() 
    {
        return true;
    }

    public uint regen() 
    {
        _Regener.value += (Time.deltaTime / tickRegenCost);
        if (_Regener.value >= 1f) 
        {
            _Regener.value = 0f;
            return regenQuantity;
        } else 
        {
            return 0;
        }
    }
}

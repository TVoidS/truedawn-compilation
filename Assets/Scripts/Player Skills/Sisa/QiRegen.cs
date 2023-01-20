using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SkillEnums;

public class QiRegen : ISpiritVeinSkills
{

    private readonly byte _ID = 0;
    public byte ID => _ID; // UNIQUE ID FOR SKILLS, Make sure all skills have a UUID.


    private byte _Level;
    public byte Level => _Level;


    private byte _Rank;
    public byte Rank => _Rank; // Initially pull from Player, but propogate back when rank changes.


    private ulong _UpgradeCost;
    public ulong UpgradeCost => _UpgradeCost; // Only set locally by calculations on start and after level up or rank up.

    public DurationType SkillType => DurationType.PassiveTimer;

    public byte _GrowthType; // Might change with certain situations?
    public GrowthType GrowthType => GrowthType.Linear;

    public string Description => "The basic ability all Spirit Veins have.  To recover Qi naturally. \n You however have the ability to recover Qi much faster. \n If you invest in this Skill that is...";

    public string Name => "Qi Regeneration";

    private byte _MaxLevel = 9;
    public byte MaxLevel => _MaxLevel;

    public QiRegen(byte level, byte rank) 
    {
        _Level = level;
        _Rank = rank;
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpiritVeinSkills
{
    public byte Level { get; }
    public byte MaxLevel { get; }
    public byte Rank { get; }
    public ulong UpgradeCost { get; }


    public byte SkillType { get; }
    public byte GrowthType { get; }


    public string Description { get; }
    public string Name { get; }
    public byte ID { get; }


    public bool RankUp(); // Return True on Success, False on Failure
    public bool LevelUp();// Return True on Success, False on Failure

}

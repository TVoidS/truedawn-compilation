using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QiRegen : MonoBehaviour, ISpiritVeinSkills
{

    private void Start { 

    }
    public byte Level { get; }
    public byte Rank { get; }
    public ulong UpgradeCost { get; }
    public byte SkillType { get; }
    public byte GrowthType { get; }


    public string Description { get; }
    public string Name { get; }
}

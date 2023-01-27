using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SkillEnums;

public class Skill
{
    public readonly DurationType SkillType;

    public readonly string Description;

    public readonly string Name;

    public readonly byte ID;

    public Skill(byte id, DurationType duration, string name, string description) 
    {
        ID = id;
        Name = name;
        Description = description;
        SkillType = duration;
    }
}

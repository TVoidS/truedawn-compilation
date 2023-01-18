using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEnums
{
    public enum GrowthType 
    {
        Linear,
        Multiplicative,
        Exponential,
        Factorial,
        Mixed
    }

    public enum DurationType
    {
        Instant,
        Toggle,
        Passive
    }
}

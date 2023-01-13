using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillGrowthTypes // TODO: Change to ENUM and combine with SkillTypes in general.
{
    public const byte Linear = 0;
    public const byte Multiplicative = 1;
    public const byte Exponential = 2;
    public const byte Factorial = 3;
    public const byte Mixed = 4;  // This is for skills that have multiple effects that scale differently
    // e.g. range scales linearly, but effect strength scales multiplicatively.
}

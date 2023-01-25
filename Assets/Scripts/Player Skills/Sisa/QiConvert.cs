using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QiConvert : ISpiritVeinSkills
{
    private byte _Level = 0;
    public byte Level => _Level;

    private byte _MaxLevel = 9;
    public byte MaxLevel => _MaxLevel;

    private byte _Rank = 0;
    public byte Rank => _Rank;

    // TODO: Make this actually calculate based on existing data
    public ulong UpgradeCost => 1;

    public SkillEnums.DurationType SkillType => SkillEnums.DurationType.DelayedInstant;

    public SkillEnums.GrowthType GrowthType => SkillEnums.GrowthType.Multiplicative;

    public string Description => "The most basic of skills for a budding Spirit Vein.  This converts your Qi into Inferior Spirit Slag!";

    public string Name => "Qi Conversion";

    // TODO: Setup an ID system!
    public byte ID => throw new System.NotImplementedException();

    public bool LevelUp()
    {
        throw new System.NotImplementedException();
    }

    public bool RankUp()
    {
        throw new System.NotImplementedException();
    }


    private Slider progressBar;
    private TMP_Dropdown matSelector;

    public QiConvert(Slider convertSlider, TMP_Dropdown convertSelector) 
    {
        progressBar = convertSlider;
        matSelector = convertSelector;
    }


    public void convert() 
    {
        if (progressBar.value >= 1) 
        {
            progressBar.value = 0;
            PlayerStats.addSlag(gains);
        } 
        else 
        {
            progressBar.value += (Time.deltaTime / timeTaken);
        }
    }

    private ulong gains = 1;
    public bool recalculateGains() 
    {
        // TODO: make this based off of the QiPurity skill level and rank.
        gains = 1;
        return true;
    }

    private float timeTaken = 10f;
    public bool recalculateTime() 
    {
        // TODO: make this based off of the QiConversion skill level and rank
        timeTaken = 10f;
        return true;
    }
}

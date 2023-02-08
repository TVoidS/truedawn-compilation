using System.Collections.Generic;
using UnityEngine;

public partial class SkillController : MonoBehaviour
{
    private void Start()
    {
        // TODO: Automatically form a collection of buttons that meet a certain criteiria (have a specific component attatched)
        // That list will be used for connecting to skills or anything else, based on the ID of the attatched component

        PlayerStats.Setup(qiCountDisplay, issDisplay, sysPointsDisp, sellSlag);

        PlayerStats.Load(qiRegenProgressBar, "");
        // Make this load from PlayerStats file
        new QiRegen(0, 0, qiRegenProgressBar, QiLevelUpTrigger.onClick);
        new QiConvert(0, 0, qiConvertSlider, qiConvertSelector, slagConvertBtn, conversionLevelUpTrigger);
        // TODO:
        // 1. Make all of this event and display stuff no longer needed, as they will use the StatDisplay.cs and SkillButton.cs files for registering buttons and displays.
        // 2. Move the object creation over to the PlayerStats.cs file.
    }


    private void Update()
    {
        RunTimerSkills();
    }
}

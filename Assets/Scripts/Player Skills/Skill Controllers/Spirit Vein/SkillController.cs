using UnityEngine;

public partial class SkillController : MonoBehaviour
{
    private void Start()
    {
        // TODO: Automatically form a collection of buttons that meet a certain criteiria (have a specific component attatched)
        // That list will be used for connecting to skills or anything else, based on the ID of the attatched component

        PlayerStats.setup(gameObject, qiCountDisplay, issDisplay, sysPointsDisp, sellSlag);

        SkillSetup();
    }

    private void SkillSetup()
    {
        // Make this load from PlayerStats file
        qiRegener = new QiRegen(0, 0, 0, qiRegenProgressBar, QiLevelUpTrigger);
        qiConverter = new QiConvert(1, 0, 0, qiConvertSlider, qiConvertSelector, slagConvertBtn, conversionLevelUpTrigger);
    }

    private void Update()
    {
        RunTimerSkills();
    }
}

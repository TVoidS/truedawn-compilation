using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillController : MonoBehaviour
{

    [Header("Spirit Vein Qi Generation")]
    public TextMeshProUGUI qiCountDisplay;
    public Slider qiRegenProgressBar;

    [Header("Spirit Vein Qi Conversion")]
    public TextMeshProUGUI issDisplay;
    public Slider qiConvertSlider;
    public Button slagConvertBtn;
    public TMP_Dropdown qiConvertSelector;

    [Header("Spirit Vein System Points")]
    public TextMeshProUGUI sysPointsDisp;
    public Button sellSlag;

    private QiRegen qiRegener;
    private QiConvert qiConverter;

    // STATIC SECTION:

    // TODO: Make this thread-safe!
    // The exception relating to the RunTimerSkills() method is due to it deleting itself while it is looking at itself!

    private static readonly List<ITimerSkill> TimerSkills = new();

    public static bool RegisterTimerSkill(ITimerSkill skill)
    {
        TimerSkills.Add(skill);
        return true;
    }

    public static bool DeregisterTimerSkill(ITimerSkill skill)
    {
        Debug.Log(TimerSkills.ToString());
        TimerSkills.Remove(skill);
        Debug.Log(TimerSkills.ToString());
        return true;
    }

    private static void RunTimerSkills()
    {
        TimerSkills.ForEach(i => i.SkillUpdate());
    }

    public static void Log(string log) 
    {
        Debug.Log(log);
    }

    // END STATIC SECTION

    private void Start()
    {
        // TODO: Automatically form a collection of buttons that meet a certain criteiria (have a specific component attatched)
        // That list will be used for connecting to skills or anything else, based on the ID of the attatched component

        PlayerStats.setup(gameObject, qiCountDisplay, issDisplay, sysPointsDisp, sellSlag);

        skillSetup();
    }

    private void skillSetup()
    {
        // Make this load from PlayerStats file
        qiRegener = new QiRegen(0, 0, 0, qiRegenProgressBar);
        qiConverter = new QiConvert(1, 0, 0, qiConvertSlider, qiConvertSelector, slagConvertBtn);
    }

    private void Update()
    {
        RunTimerSkills();
    }
}

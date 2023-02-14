using System.Collections.Generic;
using UnityEngine;

public partial class SkillController : MonoBehaviour
{
    private void Awake()
    {
        // TODO: Automatically form a collection of buttons that meet a certain criteiria (have a specific component attatched)
        // That list will be used for connecting to skills or anything else, based on the ID of the attatched component

        PlayerStats.Setup(qiCountDisplay, issDisplay, sysPointsDisp, sellSlag);
    }


    private void Update()
    {
        RunTimerSkills();
    }
}

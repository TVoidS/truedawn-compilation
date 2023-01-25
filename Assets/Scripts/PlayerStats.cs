using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class PlayerStats
{

    //TODO: Make this the entire character stat and skill screen for all games. :D


    static SkillController skill;

    // Stored values.
    // TODO: LOAD THESE FROM FILE?
    static ulong sysPoints;
    static ulong issMass;

    public static void setup(GameObject gameObject, TextMeshProUGUI qiCountDisplay, TextMeshProUGUI slagCount) 
    {
        // Load Player Data
        QiCount.initiate((ulong)0, qiCountDisplay, (ulong)10);
        sysPoints = 0;
        issMass = 0;
        slagDisplay = slagCount;
    }

    static void setupSkillController(GameObject gameObject) 
    {
        skill = gameObject.GetComponent<SkillController>();
    }

    static TextMeshProUGUI slagDisplay;
    public static void addSlag(ulong slag) 
    {
        issMass += slag;
        slagDisplay.SetText(issMass + "g");
    }

}

static class QiCount 
{
    private static ulong qi = 0;
    private static ulong max = 10;
    private static TextMeshProUGUI display;

    public static bool initiate(ulong qi, TextMeshProUGUI display, ulong max) 
    {
        QiCount.qi = qi;
        QiCount.display = display;
        return true;
    }

    public static bool add(ulong qiAdd) 
    {
        if (qi < max)
        {
            if (qi + qiAdd < max)
            {
                qi += qiAdd;
                disp();
                return true;
            }
            else
            {
                qi = max;
                disp();
                return true;
            }
        }
        else 
        {
            return false;
        }
    }

    public static bool sub(ulong qiSub) 
    {
        if (qi - qiSub > qi)
        {
            return false;
        }
        else 
        {
            qi -= qiSub;
            disp();
            return true;
        }
    }

    private static void disp() 
    {
        display.SetText(qi + "/" + max);
    }
}

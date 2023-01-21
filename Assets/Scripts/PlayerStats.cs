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

    public static void setup(GameObject gameObject) 
    {
        // Load Player Data
        QiCount.initiate((ulong)0);
        sysPoints = 0;
    }

    static void setupSkillController(GameObject gameObject) 
    {
        skill = gameObject.GetComponent<SkillController>();
    }

}

static class QiCount 
{
    private static ulong qi = 0;
    public static bool initiate(ulong qi) 
    {
        if (qi == 0)
        {
            // TODO: Checks for failure,  somehow.
            QiCount.qi = qi;
            return true;
        } 
        else
        {
            return false;
        }
    }

    public static bool add(ulong qiAdd) 
    {
        qi += qiAdd;
        return true;
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
            return true;
        }
    }
}

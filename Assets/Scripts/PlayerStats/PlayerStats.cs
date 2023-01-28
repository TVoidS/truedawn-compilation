using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class PlayerStats
{

    //TODO: Make this the entire character stat and skill screen for all games. :D

    public static void setup(GameObject gameObject, TextMeshProUGUI qiCountDisplay, TextMeshProUGUI slagCount, TextMeshProUGUI sysDisp, Button slagSell) 
    {
        // Load Player Data
        QiCount.Initiate(10, qiCountDisplay, 10);
        SystemPointsCount.Initiate(sysDisp, 0);
        SlagCount.Initiate(slagCount,slagSell, 0);
        SystemPointsCount.Display();
    }
}

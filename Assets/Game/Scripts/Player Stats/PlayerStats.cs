using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class PlayerStats
{

    //TODO: Make this the entire character stat and skill screen for all games. :D

    public static void Setup(TextMeshProUGUI qiCountDisplay, TextMeshProUGUI slagCount, TextMeshProUGUI sysDisp, Button slagSell) 
    {
        // Load Player Data
        QiCount.Initiate(10, qiCountDisplay, 10);
        SystemPointsCount.Initiate(sysDisp, 0);
        SlagCount.Initiate(slagCount,slagSell, 0);
        SystemPointsCount.Display();

        Load();

        SaveLoad.SaveCheck();

        // This is a debug line more than anything for now.
        // I use it to check the state on bootup.
        SaveLoad.Save();
    }

    private static void Load() 
    {
        // TODO: make this read from the saves files.
        // That will have to wait for when I make a main menu for the game...
        new QiConvert(0, 0);
        new QiRegen(0, 0);
        new QiPurity(0, 0);
    }
}

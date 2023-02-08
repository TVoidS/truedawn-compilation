using TMPro;

public static class QiCount
{
    private static ulong Qi = 0;
    private static ulong Max = 10;
    private static TextMeshProUGUI DisplayLbl;

    public static bool Initiate(ulong qi, TextMeshProUGUI display, ulong max)
    {
        Qi = qi;
        DisplayLbl = display;
        Display();
        return true;
    }

    public static bool Add(ulong qiAdd)
    {
        if (Qi < Max)
        {
            if (Qi + qiAdd < Max)
            {
                Qi += qiAdd;
                Display();
                return true;
            }
            else
            {
                Qi = Max;
                Display();
                return true;
            }
        }
        else
        {
            return false;
        }
    }

    public static bool Sub(ulong qiSub)
    {
        if (Qi - qiSub > Qi)
        {
            return false;
        }
        else
        {
            Qi -= qiSub;
            Display();
            return true;
        }
    }

    private static void Display()
    {
        DisplayLbl.SetText(Qi + "/" + Max);
    }
}

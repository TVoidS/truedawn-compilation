using TMPro;

public static class QiCount
{
    private static ulong _Qi = 0;
    public static ulong Qi => _Qi;

    private static ulong _Max = 10;
    public static ulong Max => _Max; // TODO: Let the Qi Capacity skill control this.

    private static bool initiated = false;

    public static bool Initiate(ulong qi, ulong max)
    {
        if (!initiated)
        {
            _Qi = qi;
            _Max = max;
            initiated = true;
            return true;
        }
        else 
        {
            return false;
        }

    }

    public static bool Add(ulong qiAdd)
    {
        if (_Qi < _Max)
        {
            if (_Qi + qiAdd < _Max)
            {
                _Qi += qiAdd;
                Display();
                return true;
            }
            else
            {
                _Qi = _Max;
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
        if (_Qi - qiSub > _Qi)
        {
            return false;
        }
        else
        {
            _Qi -= qiSub;
            Display();
            return true;
        }
    }

    public static bool NewMax(ulong newMax) 
    {
        _Max = newMax;
        return true;
    }

    public static void Display()
    {
        SkillController.UpdateTextDisplay(StatEnums.Qi, _Qi + "/" +_Max + " Qi");
    }

    /// <summary>
    /// Converts the class into a JSON format for saving.
    /// </summary>
    /// <returns> the JSON representation of the player's QiCount. </returns>
    public static string ToJson() 
    {
        string json = "{";
        json += "\"Qi\":" + Qi +",";
        json += "\"Max\":" + Max;
        json += "}";
        return json;
    }
}

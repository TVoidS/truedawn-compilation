public static class SystemPointsCount
{
    private static double systemPoints = 0;
    private static bool initiated = false;
    public static bool Initiate(ulong sysPoints) 
    {
        if (initiated)
        {
            return false;
        }
        else 
        {
            systemPoints = sysPoints;
            initiated = true;
            return true;
        }
    }

    public static void Load(double loadedSP) 
    {
        systemPoints = loadedSP;
    }

    public static bool Add(ulong spAdd) 
    {
        systemPoints += spAdd;
        Display();
        return true;
    }

    public static bool Sub(double spSub) 
    {
        if ((systemPoints - spSub) < 0)
        {
            return false;
        }
        else 
        {
            systemPoints -= spSub;
            Display();
            return true;
        }
    }

    public static void Display() 
    {
        SkillController.UpdateTextDisplay(StatEnums.SystemPoints, systemPoints + " SP");
    }

    public static string ToJson(byte tabcount) 
    {
        string tabs = "";
        for (byte i = 0; i < tabcount; i++)
        {
            tabs += "\t";
        }

        string json = tabs + "{\n" 
            + tabs + "\t\"Stat\":" + "\"SP\",\n"
            + tabs + "\t\"SP\":"+systemPoints+"\n" 
            + tabs + "}";
        return json;
    }
}

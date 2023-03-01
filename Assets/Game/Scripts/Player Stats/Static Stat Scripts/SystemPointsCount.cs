public class SystemPointsCount
{
    protected static double systemPoints = 0;
    protected static bool initiated = false;
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
            systemPoints = systemPoints - spSub;
            Display();
            return true;
        }
    }

    public static void Display() 
    {
        SkillController.UpdateTextDisplay(StatEnums.SystemPoints, systemPoints + " SP");
    }

    public static string ToJson() 
    {
        string json = "{\"SP\":"+systemPoints+"}";
        return json;
    }
}

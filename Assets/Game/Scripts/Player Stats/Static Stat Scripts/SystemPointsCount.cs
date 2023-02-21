public class SystemPointsCount
{
    protected static ulong systemPoints = 0;
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

    public static bool Sub(ulong spSub) 
    {
        if (systemPoints - spSub > systemPoints)
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
}

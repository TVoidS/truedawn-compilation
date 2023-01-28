using TMPro;

public class SystemPointsCount
{
    protected static ulong systemPoints = 0;
    protected static bool initiated = false;
    public static bool Initiate(TextMeshProUGUI countDisplay, ulong sysPoints) 
    {
        if (initiated)
        {
            return false;
        }
        else 
        {
            systemPoints = sysPoints;
            display = countDisplay;
            initiated = true;
            Display();
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

    protected static TextMeshProUGUI display;
    public static void Display() 
    {
        display.SetText("" + systemPoints);
    }
}

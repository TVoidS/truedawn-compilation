using TMPro;

public class SlagCount
{
    public enum Type 
    {
        InfereriorSpiritSlag,
        SpiritSlag,
        SuperiorSpiritSlag,
        SpiritShards,
        SpiritStones
    }

    private static ulong Slag = 0;
    private static TextMeshProUGUI DisplayLbl;

    public static bool Initiate(TextMeshProUGUI display, ulong slag)
    {
        Slag = slag;
        DisplayLbl = display;
        Display(Type.InfereriorSpiritSlag);
        return true;
    }

    public static bool Add(ulong slagAdd, Type type)
    {
        Slag += slagAdd;
        Display(type);
        return true;
    }

    public static bool Sub(ulong slagSub, Type type)
    {
        if (Slag - slagSub > Slag)
        {
            return false;
        }
        else
        {
            Slag -= slagSub;
            Display(type);
            return true;
        }
    }

    private static void Display(Type type)
    {
        DisplayLbl.SetText(Slag + "g");
    }
}

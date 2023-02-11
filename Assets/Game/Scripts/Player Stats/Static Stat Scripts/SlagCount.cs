using TMPro;
using UnityEngine.UI;

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

    public static bool Initiate(TextMeshProUGUI display, Button sellSlag, ulong slag)
    {
        Slag = slag;
        DisplayLbl = display;
        Display(Type.InfereriorSpiritSlag);

        setupSellBtn(sellSlag);

        return true;
    }

    private static void setupSellBtn(Button sellBtn) 
    {
        sellBtn.onClick.AddListener(() =>
        {
            // Check for enough slag to sell
            if (Slag >= 100)
            {
                var reward = Slag / 100;
                SystemPointsCount.Add(reward);
                Slag %= 100;
                Display(Type.InfereriorSpiritSlag);
            }
            else 
            {
                // No bonus
                SkillController.Log("Not enough SLAG");
            }
        });
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

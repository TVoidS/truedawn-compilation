using System.ComponentModel;
using TMPro;
using UnityEngine;

[DisplayName("Stat Text Display")]
public class TMPStatHook : TMPHook, IStatTextDisplay
{
    [SerializeField]
    private StatEnums displayStat;

    // Required event:
    void Awake()
    {
        Display = gameObject.GetComponent<TextMeshProUGUI>();
        SkillController.AttatchTextDisplay(this);
    }

    // IGeneralizedTextHook Implementations:
    public StatEnums Stat => displayStat;
    public DisplayEnums.TextDisplayType DisplayType => DisplayEnums.TextDisplayType.Quantity;

    public string GetText()
    {
        return Display.text;
    }

    public void SetText(string text)
    {
        Display.SetText(text);
    }
}

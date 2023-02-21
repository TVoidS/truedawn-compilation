using System.ComponentModel;
using TMPro;
using UnityEngine;

[DisplayName("Slag Text Display")]
public class TMPSlagHook : TMPHook, ISlagTextDisplay
{
    [SerializeField]
    private SlagTypes DisplayedSlagType;

    // Required event:
    void Awake()
    {
        Display = gameObject.GetComponent<TextMeshProUGUI>();
        SkillController.AttatchTextDisplay(this);
    }

    // ISlagTextDisplay implementation:
    public DisplayEnums.TextDisplayType DisplayType => DisplayEnums.TextDisplayType.Quantity;
    public StatEnums Stat => StatEnums.Slag;
    public SlagTypes SlagType => DisplayedSlagType;

    public string GetText()
    {
        return Display.text;
    }

    public void SetText(string text)
    {
        Display.SetText(text);
    }
}

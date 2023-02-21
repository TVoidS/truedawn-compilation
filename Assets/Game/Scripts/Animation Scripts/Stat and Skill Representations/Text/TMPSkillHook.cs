using System.ComponentModel;
using TMPro;
using UnityEngine;

[DisplayName("Skill Text Display")]
public class TMPSkillHook : TMPHook, ISkillTextDisplay
{
    [SerializeField]
    private SkillEnums.Skill skill;

    [SerializeField]
    private DisplayEnums.TextDisplayType textDisplayType;

    // Required event:
    void Awake()
    {
        Display = gameObject.GetComponent<TextMeshProUGUI>();
        SkillController.AttatchTextDisplay(this);
    }

    // ISkillTextDisplay Implementation:
    public DisplayEnums.TextDisplayType DisplayType => textDisplayType;
    public SkillEnums.Skill Skill => skill;

    public string GetText()
    {
        return Display.text;
    }

    public void SetText(string text)
    {
        Display.SetText(text);
    }
}

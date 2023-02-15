using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TMPStatHook : MonoBehaviour, IStatTextDisplay
{
    /// <summary>
    /// The text display that the IGeneralizedTextHook will be interacting with.
    /// </summary>
    private TextMeshProUGUI Display;

    [SerializeField] private StatEnums DisplayedStat;

    /// <summary>
    /// The display type.  
    /// This is hidden as Private to avoid editing from external sources.
    /// </summary>
    [SerializeField]
    protected DisplayEnums.TextDisplayType DisplayType;

    // Start is called before the first frame update
    void Start()
    {
        Display = GetComponent<TextMeshProUGUI>();
    }

    // IGeneralizedTextHook Implementations:
    public StatEnums Stat => DisplayedStat;
    public DisplayEnums.TextDisplayType TextDisplayType => DisplayType;

    public void SetText(string text)
    {
        Display.SetText(text);
    }

    public string GetText()
    {
        return Display.text;
    }
}

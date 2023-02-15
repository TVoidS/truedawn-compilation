using UnityEngine;
using TMPro;

/// <summary>
/// A class for allowing easy hooking of the TextMeshPro Text objects into the event handling system.
/// </summary>
public class TMPHook : MonoBehaviour, IGeneralizedTextDisplay
{
    /// <summary>
    /// The text display that the IGeneralizedTextHook will be interacting with.
    /// </summary>
    private TextMeshProUGUI Display;

    /// <summary>
    /// The display type.  
    /// This is hidden as Private to avoid editing from external sources.
    /// </summary>
    [SerializeField]
    private DisplayEnums.TextDisplayType DisplayType;

    // Start is called before the first frame update
    void Start()
    {
        Display = GetComponent<TextMeshProUGUI>();
    }

    // IGeneralizedTextHook Implementations:
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

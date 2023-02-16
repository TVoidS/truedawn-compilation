using UnityEngine;
using TMPro;
using System;

/// <summary>
/// A class for allowing easy hooking of the TextMeshPro Text objects into the event handling system.
/// </summary>
public abstract class TMPHook : MonoBehaviour, IGeneralizedTextDisplay
{
    /// <summary>
    /// The text display that the IGeneralizedTextHook will be interacting with.
    /// </summary>
    private TextMeshProUGUI display;

    /// <summary>
    /// The publicly accessible property for the Text Display.
    /// </summary>
    public TextMeshProUGUI Display { get { return display; } private set { display = value; } }

    /// <summary>
    /// The display type.  
    /// This is hidden as Private to avoid editing from external sources.
    /// </summary>
    [SerializeField]
    protected DisplayEnums.TextDisplayType displayType;


    // Start is called before the first frame update
    void Start()
    {
        display = GetComponent<TextMeshProUGUI>();
    }

    // IGeneralizedTextHook Implementations:
    public virtual DisplayEnums.TextDisplayType DisplayType { get { return displayType; } protected set { displayType = value; } }

    public void SetText(string text) 
    {
        display.SetText(text);
    }

    public string GetText() 
    {
        return display.text;
    }
}

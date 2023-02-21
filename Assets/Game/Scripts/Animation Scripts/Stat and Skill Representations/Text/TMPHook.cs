using UnityEngine;
using TMPro;

/// <summary>
/// A class for allowing easy hooking of the TextMeshPro Text objects into the event handling system.
/// </summary>
public abstract class TMPHook : MonoBehaviour
{
    /// <summary>
    /// The text display that the IGeneralizedTextHook will be interacting with.
    /// </summary>
    private TextMeshProUGUI display;

    /// <summary>
    /// The internally accessible property for the Text Display.
    /// </summary>
    protected TextMeshProUGUI Display { get { return display; } set { display = value; } }
}

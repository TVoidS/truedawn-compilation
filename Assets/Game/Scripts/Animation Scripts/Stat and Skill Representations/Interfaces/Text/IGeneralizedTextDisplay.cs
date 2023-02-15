/// <summary>
/// A generalized Interface for connecting any editable text displays to the custom event system.
/// </summary>
public interface IGeneralizedTextDisplay
{
    /// <summary>
    /// The type of data that the text will be displaying.
    /// This uses the DisplayEnums static class, so you will need to do so as well to interact with this correctly.
    /// </summary>
    public DisplayEnums.TextDisplayType TextDisplayType { get; }

    /// <summary>
    /// Gets the currently displayed Text.
    /// This is not likely to be used, and may be removed later.
    /// If you need this function, please message TVoidS about it.
    /// </summary>
    /// <returns> The current text of the display. </returns>
    public string GetText();

    /// <summary>
    /// Sets the display's text to what is recieved.
    /// </summary>
    /// <param name="text"> The new text for the display </param>
    public void SetText(string text);
}

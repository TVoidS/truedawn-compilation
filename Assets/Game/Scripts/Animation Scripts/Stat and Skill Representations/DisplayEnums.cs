/// <summary>
/// A collection of Enums for different display types.
/// </summary>
public static class DisplayEnums
{
    /// <summary>
    /// The type of display that the Text field will handle.
    /// This will be used when choosing what data to put into the display.
    /// If anything is displaying text that has potential to change, but doesn't fit the given types, feel free to add more.
    /// </summary>
    public enum TextDisplayType 
    {
        Level,
        LevelCost,
        Quantity,
        RankFancy,
        Name,
        Description
    }
}

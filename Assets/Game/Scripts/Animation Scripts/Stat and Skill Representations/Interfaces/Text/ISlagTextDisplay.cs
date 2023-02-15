/// <summary>
/// An addition to the IStatTextDisplay interface that allows it to specify which type of slag is being displayed.
/// </summary>
public interface ISlagTextDisplay : IStatTextDisplay
{
    /// <summary>
    /// The type of slag that the text will be associated with.
    /// This is only helpful for slag, but it will be helpful with all types of slag, stones, and anything that is treated the same.
    /// </summary>
    public SlagTypes SlagType { get; }
}

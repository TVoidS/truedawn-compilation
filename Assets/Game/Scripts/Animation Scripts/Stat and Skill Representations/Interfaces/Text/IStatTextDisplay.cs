/// <summary>
/// A version of the IGeneralizedTextDisplay for the purpose of displaying Stat related data.
/// </summary>
public interface IStatTextDisplay : IGeneralizedTextDisplay
{
    /// <summary>
    /// The stat that the Text Display will be connected to.
    /// </summary>
    public StatEnums Stat { get; }
}

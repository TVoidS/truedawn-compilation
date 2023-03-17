using System.ComponentModel;

public enum StatEnums
{
    [Description("Qi")]
    Qi,
    [Description("System Points")]
    SystemPoints,
    [Description("Slag")]
    Slag
}

/// <summary>
/// This is specifically used when something is using the StatEnums.Slag.
/// This is for helping with the definition of slag type.
/// </summary>
public enum SlagTypes 
{
    [Description("Inferior Spirit Slag")]
    InferiorSlag,
    [Description("Spirit Slag")]
    Slag,
    [Description("Superior Spirit Slag")]
    SuperiorSlag,
    [Description("Spirit Stone Fragments")]
    StoneFragments,
    [Description("Spirit Stone Shards")]
    StoneShards,
    [Description("Inferior Spirit Stones")]
    InferiorStones,
    [Description("Spirit Stones")]
    Stones,
    [Description("Superior Spirit Stones")]
    SuperiorStones,
    [Description("Spirt Seeds")]
    Seeds
}

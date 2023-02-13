/// <summary>
/// An Interface for Generalized Display objects.
/// Any object with this Interface should be able to use a Value and MaxValue to make a functional progress bar-like display.
/// The current bar's fill will always display at a percentage equivalent to Value / MaxValue
/// The specifics of how a specific implementation of the Interface will be left up to the implementation.
/// </summary>
public interface IGeneralizedProgressDisplay
{
    /// <summary>
    /// The Max Value of the display.
    /// Useful for Viewing the units of fill for your display.
    /// Use the UpdateMaxValue(newMax) mehtod to change the value.
    /// </summary>
    public float MaxValue { get; }

    /// <summary>
    /// The Current Value of the display.
    /// Useful for reading the current progress of the bar.
    /// Use the UpdateValue(newValue) method to change the current Value.
    /// </summary>
    public float Value { get; }

    /// <summary>
    /// Allows the update of the Value and updates the physical display.
    /// </summary>
    /// <param name="newValue"> The new Value of the display. </param>
    public void UpdateValue(float newValue);

    /// <summary>
    /// RECOMMENDED TO NOT USE
    /// Allows the simple addition of values to the progress bar.
    /// Simpler to use, but doesn't allow for checking of the current value.
    /// Best to leave input as seconds.
    /// </summary>
    /// <param name="value"></param>
    public void AddValue(float value);
     
    /// <summary>
    /// Allows the control of the MaxValue of the display.
    /// It will update the display with the progress of the new MaxValue.
    /// It is recommended to maintain a usage of Seconds for the display.
    /// </summary>
    /// <param name="newMax"> The new MaxValue of the Progress Bar. </param>
    public void UpdateMaxValue(float newMax);

    /// <summary>
    /// The skill that the display is meant to represent progress for.
    /// This is what it will tell the SkillController to list it under when it sends out updates.
    /// </summary>
    public SkillEnums.Skill DisplayedSkill { get; }
}

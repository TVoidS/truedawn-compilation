public interface ISkillEventResponder
{
    /// <summary>
    /// The trigger for the SkillController to use.
    /// This will trigger when a SkillEventCallback is found to apply.
    /// </summary>
    public void Trigger<T>(T input);

    /// <summary>
    /// The trigger for the SkillController to use when there is no data to forward.
    /// This will trigger when a SkillEventCallback is found to apply.
    /// </summary>
    public void Trigger();
}

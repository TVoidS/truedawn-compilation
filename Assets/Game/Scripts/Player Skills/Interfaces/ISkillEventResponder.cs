public interface ISkillEventResponder
{
    /// <summary>
    /// The trigger for the SkillController to use.
    /// This will trigger when a SkillEventCallback is found to apply.
    /// </summary>
    public void Trigger();
}

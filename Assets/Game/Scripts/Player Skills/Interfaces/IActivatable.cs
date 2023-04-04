public interface IActivatable
{
    /// <summary>
    /// The Activation method.  Normally represents an attempt at beginning the skill.
    /// This is for interfacing with the Button event handler system.
    /// </summary>
    public bool Activate();
}

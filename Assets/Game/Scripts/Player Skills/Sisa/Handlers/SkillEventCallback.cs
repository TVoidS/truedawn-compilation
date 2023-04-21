public class SkillEventCallback
{
    public SkillEnums.Skill TriggerSkill { get; }

    public SkillEnums.ButtonEvent TriggerEvent { get; }

    ISkillEventResponder Handler;

    public void Trigger(string? input) 
    {
        Handler.Trigger(TriggerEvent, input);
    }

    public SkillEventCallback(SkillEnums.Skill triggerSkill, ISkillEventResponder handler, SkillEnums.ButtonEvent triggerEvent)
    {
        TriggerSkill = triggerSkill;
        Handler = handler;
        TriggerEvent = triggerEvent;
    }
}
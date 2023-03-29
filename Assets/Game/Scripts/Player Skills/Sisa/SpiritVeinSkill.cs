using static SkillEnums;

public class SpiritVeinSkill : Skill
{
    public SpiritVeinSkill(SkillEnums.Skill id, DurationType duration, string name, string description) : base(id, duration, name, description) 
    {
        // TODO: determine if this is needed anymore!
        // May be needed for some Spirit Vein unique stuff later, but i don't know yet.
        // This used to do what the ILevelable interface now does
    }
}

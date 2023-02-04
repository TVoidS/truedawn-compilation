using static SkillEnums;

public class Skill
{
    public readonly DurationType SkillType;

    public readonly string Description;

    public readonly string Name;

    public readonly SkillEnums.Skill ID;

    public Skill(SkillEnums.Skill id, DurationType duration, string name, string description) 
    {
        ID = id;
        Name = name;
        Description = description;
        SkillType = duration;
    }
}

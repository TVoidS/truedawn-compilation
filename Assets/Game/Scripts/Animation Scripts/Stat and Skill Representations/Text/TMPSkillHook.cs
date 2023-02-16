using UnityEngine;

public class TMPSkillHook : TMPHook, ISkillTextDisplay
{
    [SerializeField]
    private SkillEnums.Skill skill;

    public SkillEnums.Skill Skill => skill;
}

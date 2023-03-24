using System.Text.Json;
using static SkillEnums;

public class Skill
{
    /// <summary>
    /// Probably Userped by the interfaces.
    /// </summary>
    public readonly DurationType SkillType;

    /// <summary>
    /// The Description of the skill.
    /// This is primarily used for display purposes.
    /// I may need to make this privately editable later though.
    /// </summary>
    public readonly string Description;

    /// <summary>
    /// The Display Name of the skill.  
    /// Primarily used for display purposes.
    /// </summary>
    public readonly string Name;

    /// <summary>
    /// The ID of the skill.  Cannot change.
    /// </summary>
    public readonly SkillEnums.Skill ID;

    public Skill(SkillEnums.Skill id, DurationType duration, string name, string description)
    {
        ID = id;
        Name = name;
        Description = description;
        SkillType = duration;
    }

    /// <summary>
    /// Converts the Skill to a JSON formatted string.
    /// If your skills is only saving the ID, then you need to override this and set your own save method.
    /// </summary>
    /// <returns> The JSON formatted version of the Skill. </returns>
    public virtual string Save(byte tabcount)
    {
        string tabs = "";
        for (byte i = 0; i < tabcount; i++)
        {
            tabs += "\t";
        }

        string ret = tabs + "{\n"
            + "\t\"ID\":\"" + ID + "\"\n"
            + "}";
        return ret;
    }

    /// <summary>
    /// Updates all displays attatched to the skill.
    /// </summary>
    public virtual void UpdateAllText()
    {
        // This implementation is primarily for the children to override.
        // This "skill" only has a name and description, so that is all it updates
        SkillController.UpdateTextDisplay(ID, DisplayEnums.TextDisplayType.Name, Name);
        SkillController.UpdateTextDisplay(ID, DisplayEnums.TextDisplayType.Description, Description);
    }

    /// <summary>
    /// Loads the skill's data from the provided JsonElement
    /// </summary>
    /// <param name="skillData"> The JsonElement representation of the saved skill. </param>
    public virtual void Load(JsonElement skillData) 
    {
        // Nothing to do here.  All data is pre-built and not related to the save. 
    }
}

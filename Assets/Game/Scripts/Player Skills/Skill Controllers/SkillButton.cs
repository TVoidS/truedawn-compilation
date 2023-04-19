using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    /// <summary>
    /// Enumerated Skill ID for the skill associated to the event to be triggered.
    /// </summary>
    public SkillEnums.Skill SkillID;

    /// <summary>
    /// Enumerated Event ID for the type of event that will be triggered.
    /// </summary>
    public SkillEnums.ButtonEvent EventType;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() => 
        {
            // On click event.  Notify the Skill Controller
            SkillController.ButtonEvent(SkillID, EventType, null);
        });
    }
}
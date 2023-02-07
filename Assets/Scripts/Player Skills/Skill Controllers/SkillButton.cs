using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{

    private Button btn;

    public SkillEnums.Skill SkillID;
    public SkillEnums.ButtonEvent EventType;


    // Start is called before the first frame update
    void Start()
    {
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(() => 
        {
            // On click event.  Notify the Skill Controller
            SkillController.ButtonEvent(SkillID, EventType);
        });
    }
}

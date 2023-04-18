using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QiTypeOptions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TMP_Dropdown dropdown = GetComponent<TMP_Dropdown>();
        List<TMP_Dropdown.OptionData> options = new();
        foreach (SlagTypes type in Enum.GetValues(typeof(SlagTypes))) 
        {
            TMP_Dropdown.OptionData optionData = new()
            {
                text = type.ToDiscriptionString()
            };
            options.Add(optionData);
        }

        dropdown.options.AddRange(options);

        dropdown.onValueChanged.AddListener(x => 
        {
            QiConvert convert = (QiConvert)SkillController.GetSkill(SkillEnums.Skill.QiConvert);
            convert.SetNextType(dropdown.options[dropdown.value].text);
        });
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static SkillEnums;

public class QiConvert : SpiritVeinSkill
{
    /// <summary>
    /// The bar that shows how close to done we are
    /// </summary>
    private Slider progressBar;

    /// <summary>
    /// The dropdown that shows what resource we are going to convert for the upcoming batch.
    /// </summary>
    private TMP_Dropdown matSelector;

    /// <summary>
    /// Construct the Qi Conversion skill.
    /// This should only be run ONCE.  And even then, only from the SkillController.
    /// </summary>
    /// <param name="id"> The Skill's unique ID </param>
    /// <param name="level"> The player's level in the skill </param>
    /// <param name="rank"> The player's rank in the skill </param>
    /// <param name="convertSlider"> The worldspace slider that represents the progress </param>
    /// <param name="convertSelector"> The worldspace dropdown that represents what material is to be generated </param>
    /// <param name="convertTrigger"> The worldspace button that triggers the conversion process to begin </param>
    public QiConvert(byte id, byte level, byte rank, Slider convertSlider, TMP_Dropdown convertSelector, Button convertTrigger) : 
        base(id,
             DurationType.DelayedInstant, // Skill's trigger/duration type, tells how to treat the trigger event
             "Qi Conversion",
             "This skill converts your Qi into Spirit Slag of various types!",
             level,
             9, // Skill's Max Level!
             rank,
             GrowthType.Linear) // Skill's stat growth type for the purposes of number calculation later!
    {
        setupConvert(convertTrigger);
        progressBar = convertSlider;
        matSelector = convertSelector;
    }

    /*
     * Delete if not needed by the end!
     * 
    public bool LevelUp()
    {
        Debug.Log("Attempted Level UP!");
        return false;
    }

    public bool RankUp()
    {
        throw new System.NotImplementedException();
    }
    */

    private Button lvlUpBtn;
    private void LevelUpSetup(Button levelUpButton) 
    {
        lvlUpBtn = levelUpButton.GetComponent<Button>();
        
        lvlUpBtn.onClick.AddListener(() => 
        {
            if (LevelUp())
            {
                Debug.Log("Level Up Worked!");
            }
            else 
            {
                Debug.Log("Level Up Failed!");
            }
        });
    }
    
    private void setupConvert(Button trigger) 
    {
        trigger.onClick.AddListener(() => 
        {
            // TODO: register itself to the passive list 
            if (QiCount.sub(1))
            {
                convert();
            }
            else 
            {
                //Not enough QI
                Debug.Log("Not Enough QI!");
            }
        });
    }
    public void convert() 
    {
        if (progressBar.value >= 1) 
        {
            progressBar.value = 0;
            PlayerStats.addSlag(gains);
        } 
        else 
        {
            progressBar.value += (Time.deltaTime / timeTaken);
        }
    }

    private ulong gains = 1;
    public bool recalculateGains() 
    {
        // TODO: make this based off of the QiPurity skill level and rank.
        gains = 1;
        return true;
    }

    private float timeTaken = 10f;
    public bool recalculateTime() 
    {
        // TODO: make this based off of the QiConversion skill level and rank
        timeTaken = 10f;
        return true;
    }
}

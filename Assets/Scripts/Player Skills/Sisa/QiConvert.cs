using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static SkillEnums;

public class QiConvert : SpiritVeinSkill, ITimerSkill
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
        SetupConvert(convertTrigger);
        progressBar = convertSlider;
        matSelector = convertSelector;
    }

    /// <summary>
    /// The status of the conversion process.
    /// True if the progress bar is filling up.
    /// </summary>
    private bool Converting = false;
    /// <summary>
    /// This Method adds the Listener that will register the skill to update when the provided button is clicked.
    /// </summary>
    /// <param name="trigger"> The button that will trigger the timer event for the skill </param>
    private void SetupConvert(Button trigger) 
    {
        trigger.onClick.AddListener(() => 
        {
            // TODO: register itself to the passive list 
            if (Converting)
            {
                // fail.
                Debug.Log("BUSY.  Hold your horses!");
            }
            else if (QiCount.sub(1))
            {
                // TODO: change the 1 in sub() to QiCost for dynamic cost caluclations
                // Start Converting!
                Converting = true;
                SkillController.RegisterTimerSkill(this);
            }
            else
            {
                // Too broke
                Debug.Log("Not Enough Qi");
            }
        });
    }

    // Interface Implementation from ITimerSkill
    public void SkillUpdate()
    {

        // This just checks if the bar is done, then finishes the convert.
        // It then removes itself from the TimerSkills list in SkillController.  This may need to be Thread-Safe later...
        if (progressBar.value >= 1)
        {
            progressBar.value = 0;
            PlayerStats.addSlag(gains);
            Converting = false;
            SkillController.DeregisterTimerSkill(this);
        }
        else
        {
            progressBar.value += (Time.deltaTime / timeTaken);
        }
    }


    private ulong gains = 1;
    public bool RecalculateGains() 
    {
        // TODO: make this based off of the QiPurity skill level and rank.
        gains = 1;
        return true;
    }

    private float timeTaken = 10f;
    public bool RecalculateTime() 
    {
        // TODO: make this based off of the QiConversion skill level and rank
        timeTaken = 10f;
        return true;
    }
}

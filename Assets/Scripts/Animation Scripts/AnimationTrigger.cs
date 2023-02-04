using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AnimationTrigger : MonoBehaviour {
    /// <summary>
    /// The animator of the attatched object
    /// </summary>
    private Animator anim;

    /// <summary>
    /// The enumerator ID of the skill that triggers the animation
    /// </summary>
    public SkillEnums.Skill TriggerSkill;

    /// <summary>
    /// Runs on Unity's startup if the script is attatched to any gameobject.
    /// </summary>
    void Start () {
        anim = GetComponent<Animator>();
        SkillController.RegisterAnim(new AnimTriggerCallback(this, TriggerSkill));
    }


    private float AnimTime;

    /// <summary>
    /// Triggers the Animation of the attatched object for a fixed amount of time
    /// </summary>
    /// <param name="time"> The duration of the animation </param>
    public void TriggerAnim(float time) 
    {
        //AnimTime = time/3f;
        //anim.PlayInFixedTime("startRotation", -1, AnimTime);
        
        anim.Play("startRotation");
        anim.SetInteger("repeat", (int)(time/2f));
        //Invoke("AnimOffsetLoop", AnimTime);
       
    }

    private void AnimOffsetLoop() 
    {
        float multi = 1f;
        //anim.PlayInFixedTime("rotationLoop", -1, AnimTime*multi);
        Invoke("AnimOffsetEnd", AnimTime*multi);
    }

    private void AnimOffsetEnd()
    {
        //anim.PlayInFixedTime("endRotation", -1, AnimTime);
    }
}

public class AnimTriggerCallback 
{
    /// <summary>
    /// The AnimationTrigger class that holds all the information regarding the animation
    /// </summary>
    public AnimationTrigger Anim { get; }

    /// <summary>
    /// The ID of the skill that triggers the animation.
    /// </summary>
    public SkillEnums.Skill TriggerSkill { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="anim"> The AnimationTrigger that holds all the information regarding the animation </param>
    /// <param name="trigger"> The ID of the skill that triggers the animation. </param>
    public AnimTriggerCallback(AnimationTrigger anim, SkillEnums.Skill trigger) 
    {
        Anim = anim;
        TriggerSkill = trigger;
    }
}
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
        UpdateAnimTimes();
    }

    /// <summary>
    /// Triggers the Animation of the attatched object for a fixed amount of time
    /// </summary>
    /// <param name="time"> The duration of the animation </param>
    public void TriggerAnim(float time) 
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Cube.idle"))
        {
            int repeat = (int) ((time - 4f)/2f);
            anim.SetInteger("repeat", repeat);
            anim.Play("startRotation");
        }
    }


    /// <summary>
    /// TODO:
    /// </summary>
    private float AnimDuration = 0f;
    public void UpdateAnimTimes() 
    {
        AnimDuration = 0f;
        foreach (AnimationClip AnimClip in anim.runtimeAnimatorController.animationClips)
        {
            AnimDuration += AnimClip.length * anim.speed;
        }
    }
}
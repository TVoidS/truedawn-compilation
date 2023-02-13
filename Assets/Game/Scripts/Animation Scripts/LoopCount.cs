using UnityEngine;

public class LoopCount : StateMachineBehaviour
{
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Decrement the repeat value.
        animator.SetInteger("repeat", animator.GetInteger("repeat") - 1);
    }
}

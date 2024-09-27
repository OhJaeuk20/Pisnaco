using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiveRollSmb : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<PlayerMovement>().IsDiveRoll = true;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<PlayerMovement>().DiveRollMove();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<PlayerMovement>().IsDiveRoll = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverSmb : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponentInParent<TimeObjController>().isActive = true;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponentInParent<TimeObjController>().DelayDestroyObject();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaySmb : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponentInParent<TimeObjController>().boxCollider.enabled = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponentInParent<TimeObjController>().boxCollider.enabled = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActionSmb : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<BossActionState>().aniEnd = true;
    }
}

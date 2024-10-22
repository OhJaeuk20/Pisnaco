using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontTurnSmb : StateMachineBehaviour
{
    public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        animator.GetComponent<BossAttackState>().dontTurn = true;
    }

    public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        animator.GetComponent<BossAttackState>().dontTurn = false;
    }
}

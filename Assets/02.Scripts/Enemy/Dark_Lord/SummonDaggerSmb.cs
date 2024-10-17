using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonDaggerSmb : StateMachineBehaviour
{
    public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        animator.GetComponent<SummonWeapon>().SummonDaggers();
    }

    public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        animator.GetComponent<SummonWeapon>().Unsummon();
    }
}

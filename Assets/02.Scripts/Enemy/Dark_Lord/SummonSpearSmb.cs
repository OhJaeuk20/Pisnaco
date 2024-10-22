using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonSpearSmb : StateMachineBehaviour
{
    public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        animator.GetComponent<SummonWeapon>().SummonSpear();
    }

    public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        animator.GetComponent<SummonWeapon>().Unsummon();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonSwordSmb : StateMachineBehaviour
{
    public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        animator.GetComponent<SummonWeapon>().SummonSword();
    }

    public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        animator.GetComponent<SummonWeapon>().Unsummon();
    }
}

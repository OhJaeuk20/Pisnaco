using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastingSmb : StateMachineBehaviour
{
    public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        animator.GetComponent<MonsterSkillController>().SetIsCasting(true);
    }

    public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        animator.GetComponent<MonsterSkillController>().SetIsCasting(false);
        animator.GetComponent<MonsterSkillController>().NextSkill = null;
    }
}
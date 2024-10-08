using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboSmb : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.GetComponent<MonsterSkillController>().IsCastingToggle();
       animator.GetComponent<BossAttackState>().ToggleDontTurn();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<MonsterSkillController>().IsCastingToggle();
        animator.GetComponent<BossAttackState>().ToggleDontTurn();
    }
}

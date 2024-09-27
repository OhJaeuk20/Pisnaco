using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChargeAttackSmb : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<ChasePlayer>().FixRotationSwitch();
        animator.GetComponent<ChasePlayer>().nvAgent.enabled = false;
        animator.GetComponent<SkeletonChargeAttack>().sphereCollider.enabled = true;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<SkeletonChargeAttack>().Charge();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<ChasePlayer>().FixRotationSwitch();
        animator.GetComponent<ChasePlayer>().nvAgent.enabled = true;
        animator.GetComponent<SkeletonChargeAttack>().sphereCollider.enabled = false;
        animator.GetComponent<MonsterSkillControler>().DelayCastingEnd();
    }
}

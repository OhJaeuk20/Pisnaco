using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NormalMeleeAttackSmb : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<ChasePlayer>().FixRotationSwitch();
        animator.GetComponent<ChasePlayer>().nvAgent.isStopped = true;
        animator.GetComponent<NormalMeleeAttack>().sphereCollider.enabled = true;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<ChasePlayer>().FixRotationSwitch();
        animator.GetComponent<NormalMeleeAttack>().sphereCollider.enabled = false;
        animator.GetComponent<MonsterSkillControler>().DelayCastingEnd();       
    }
}
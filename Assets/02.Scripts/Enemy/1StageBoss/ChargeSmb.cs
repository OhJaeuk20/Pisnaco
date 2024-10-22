using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChargeSmb : StateMachineBehaviour
{
    private NavMeshAgent navMeshAgent;
    private float chargeSpeed;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        navMeshAgent = animator.GetComponent<NavMeshAgent>();
        chargeSpeed = animator.GetComponent<SkeletonChargeAttack>().chargeSpeed;

        animator.GetComponent<SkeletonChargeAttack>().StartChargeAttack();
        animator.GetComponent<SkeletonChargeAttack>().boxCollider.enabled = true;
        animator.GetComponent<SkeletonChargeAttack>().capsuleCollider.enabled = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        navMeshAgent.Move(animator.transform.forward * chargeSpeed * Time.deltaTime);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<MonsterSkillController>().ResetAttackDistance();
        animator.GetComponent<SkeletonChargeAttack>().boxCollider.enabled = false;
        animator.GetComponent<SkeletonChargeAttack>().capsuleCollider.enabled = true;
    }
}
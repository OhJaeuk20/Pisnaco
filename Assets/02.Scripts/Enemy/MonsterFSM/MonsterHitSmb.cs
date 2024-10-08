using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 대기 애니메이션 상태 머신
public class MonsterHitSmb : StateMachineBehaviour
{
    private MonsterHealth health;

    private NavMeshAgent navMeshAgent;

    [SerializeField] protected float knockbackTime;
    [SerializeField] protected float knockbackForce;
    protected float time;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        health = animator.GetComponent<MonsterHealth>();
        navMeshAgent = animator.GetComponent<NavMeshAgent>();

        health.IsHit = true;
        time = 0;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time += Time.deltaTime;

        if (time < knockbackTime)
        {
            navMeshAgent.Move(-animator.transform.forward * knockbackForce * Time.deltaTime);
            return;
        }

        time = 0;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        navMeshAgent.isStopped = false;
        health.IsHit = false;
    }
}

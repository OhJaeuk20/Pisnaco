using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 몬스터 추적 상태
public class MonsterDetectState : MonsterState
{
	// 추적 상태 시작
	public override void EnterState(MonsterFSMController.STATE state)
	{
        // 추적 속도 설정
        navMeshAgent.speed = fsmInfo.DetectMoveSpeed;
        // 추적 애니메이션 재생
        animator.SetInteger("State", (int)state);
    }

	// 추적 상태 진행
	public override void UpdateState()
	{
        // 추적하던 공격 대상이 공격 가능 거리안으로 들어왔다면
        if (controller.GetPlayerDistance() <= fsmInfo.AttackDistance)
        {
            // 공격 상태로 전환함
            controller.TransactionToState(MonsterFSMController.STATE.ATTACK);
            return;
        }

        // 추적 대상이 추적 가능거리를 넘어서 도망갔다면
        if (controller.GetPlayerDistance() > fsmInfo.DetectDistance)
        {
            // 기지(배회 위치)로 복귀함
            controller.TransactionToState(MonsterFSMController.STATE.GIVEUP);
            return;
        }

        // 공격 대상 추적 처리
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(controller.Player.transform.position);
    }

	// 추적 상태 종료
	public override void ExitState()
	{
        animator.speed = 1; // (임시) 애니메이션 재생 속도 복원
    }
}

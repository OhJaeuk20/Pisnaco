using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 몬스터 공격 상태
public class MonsterActionState : MonsterState
{
    [SerializeField] private GameObject fxPrefab;
    public bool aniEnd = false;

    // 공격 상태 시작
    public override void EnterState(MonsterFSMController.STATE state)
	{
        base.EnterState(state);
        // 공격을 위해 이동 중지
        NavigationStop();

        // 공격 상태 애니메이션 재생
        animator.SetInteger("State", (int)state);
    }

	// 공격 상태 진행
	public override void UpdateState()
	{
        if (!aniEnd) return;
        
        // 맞고 있는 상태가 아니고 공격가능한 상태면
        if (controller.GetPlayerDistance() <= fsmInfo.AttackDistance)
        {
            // 공격 상태로 전환
            controller.TransactionToState(MonsterFSMController.STATE.ATTACK);
            return;
        }

        // 플레이어와의 거리가 추적해야할 거리면
        if (controller.GetPlayerDistance() <= fsmInfo.DetectDistance)
        {
            // 추적상태로 전환
            controller.TransactionToState(MonsterFSMController.STATE.DETECT);
            return;
        }
    }

	// 공격 상태 종료
	public override void ExitState()
	{
        aniEnd = false;
    }

    
}

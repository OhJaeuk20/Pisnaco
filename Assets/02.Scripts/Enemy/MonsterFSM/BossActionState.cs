using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 몬스터 공격 상태
public class BossActionState : MonsterState
{
    public bool aniEnd = false;
    private BossHealth health;

    void Start()
    {
        health = GetComponent<BossHealth>();
    }

    // 공격 상태 시작
    public override void EnterState(MonsterFSMController.STATE state)
	{
        base.EnterState(state);
        // 공격을 위해 이동 중지
        NavigationStop();

        // 공격 상태 애니메이션 재생
        animator.SetInteger("Phase", (int)health.currentPhase);
        animator.SetInteger("State", (int)state);
        animator.SetTrigger("Action");
    }

	// 공격 상태 진행
	public override void UpdateState()
	{
        LookAtTarget();
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


    protected void LookAtTarget()
    {
        // 공격 대상을 향한 방향을 계산
        Vector3 direction = (controller.Player.transform.position - transform.position).normalized;

        // 회전 쿼터니언 계산
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));

        // 보간 회전
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * fsmInfo.LookAtMaxSpeed);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 몬스터 피격 상태
public class MonsterHitState : MonsterState
{
	// 피격 상태
    [SerializeField] private bool isHit;
    public bool IsHit { get => isHit; set => isHit = value; }

	// 피격 파티클 컴포넌트
	[SerializeField] protected ParticleSystem hitParticle;

    // 체력 컴포넌트
	private MonsterHealth health;

    // 피격 넉백 시간
    [SerializeField] protected float knockbackTime;
    // 피격 넉백 힘
    [SerializeField] protected float knockbackForce;

    protected override void Awake()
    {
        base.Awake();

        health = GetComponent<MonsterHealth>();
    }

    //  피격 상태 시작
    public override void EnterState(MonsterFSMController.STATE state)
	{
        base.EnterState(state);

        // 이동 중지
        navMeshAgent.isStopped = true;

        // 피격 효과 처리
        //hitParticle.Play();

        // 피격 애니메이션 재생
        animator.SetInteger("State", (int)state);

        // 피격 넉백 처리 코루틴 생성
        StartCoroutine(ApplyHitKnockback(-transform.forward));
    }

	// 피격 상태 진행
	public override void UpdateState()
	{
        // 이미 피격이 진행 중이면 패쓰
        if (health.IsHit)
        {
            return;
        }

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

	// 피격 상태 종료
	public override void ExitState()
	{
        health.IsHit = false;
    }

    // 넉백 처리 코루틴
    private IEnumerator ApplyHitKnockback(Vector3 hitDirection)
    {
        // 피격 상태 잠금
        health.IsHit = true;
        // 네비게이션 중지
        navMeshAgent.isStopped = true;

        // 넉백 이동 처리 진행
        float timer = 0f;
        while (timer < knockbackTime)
        {
            navMeshAgent.Move(hitDirection * knockbackForce * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }

        // 네비게이션 재가동
        navMeshAgent.isStopped = false;
        // 피격 상태 해제
        health.IsHit = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 몬스터 사망 상태
public class MonsterDeathState : MonsterState
{
    // 사망 완료 처리 시간
    protected float time;

    [SerializeField] protected float deathDelayTime;

    // 피격 파티클 컴포넌트
    [SerializeField] protected ParticleSystem hitParticle;

    // 사망 처리 이펙트
    [SerializeField] protected GameObject destroyParticlePrefab;

    protected Transform destroyParticleTr;

    // 피격 넉백 시간
    [SerializeField] protected float knockbackTime;
    // 피격 넉백 힘
    [SerializeField] protected float knockbackForce;

    // 시체 가라앉는 속도
    [SerializeField] protected float sinkSpeed;

    //  사망 상태 시작
    public override void EnterState(MonsterFSMController.STATE state)
	{
        base.EnterState(state);

        // 피격 효과 처리
        hitParticle.Play();

        // 사망 애니메이션 재생
        animator.SetBool("Dead", true);

        // 피격 넉백 처리 코루틴 생성
        StartCoroutine(ApplyDeathKnockback(-transform.forward));
    }

	// 사망 상태 진행
	public override void UpdateState()
	{
        time += Time.deltaTime;

        // 사망 처리 지연시간이 지났다면
        if (time >= deathDelayTime)
        {
            ExitState();
        }
    }

	// 사망 상태 종료
	public override void ExitState()
	{
        // 몬스터가 소멸됨
        Destroy(gameObject);
    }

    // 넉백 처리 코루틴
    private IEnumerator ApplyDeathKnockback(Vector3 hitDirection)
    {
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
        
    }

    public void StartSinkingCoroutine()
    {
        navMeshAgent.enabled = false;
        Instantiate(destroyParticlePrefab, transform.position, Quaternion.identity);
        StartCoroutine(SinkCoroutine());
    }

    private IEnumerator SinkCoroutine()
    {
        float sinkDuration = 5f;
        float elapsedTime = 0f;

        while (elapsedTime < sinkDuration)
        {
            // y축으로 천천히 가라앉음
            transform.position -= new Vector3(0, sinkSpeed * Time.deltaTime, 0);
            elapsedTime += Time.deltaTime;
            yield return null;  // 한 프레임 기다림
        }
    }
}

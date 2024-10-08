using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 몬스터 배회 상태
public class MonsterRoamingState : MonsterState
{
    // 배회 위치 게임오브젝트 참조
    protected Transform targetTransform = null;
    // 배회 위치 (기본 : 무한 위치값)
    public Vector3 targetPosition = Vector3.positiveInfinity;
    public float targetDistance = Mathf.Infinity;

    // 배회 상태 시작
    public override void EnterState(MonsterFSMController.STATE state)
    {
        base.EnterState(state);

        // 배회 애니메이션 재생
        animator.SetInteger("State", (int)state);
    }

    // 배회 상태 진행
    public override void UpdateState()
    {
        // 플레이어가 공격 가능 거리안에 들어오면
        if (controller.GetPlayerDistance() <= fsmInfo.AttackDistance)
        {
            // 공격 상태로 전환
            controller.TransactionToState(MonsterFSMController.STATE.ATTACK);
            return;
        }

        // 플레이어가 추적 가능 거리안에 들어오면
        if (controller.GetPlayerDistance() <= fsmInfo.DetectDistance)
        {
            // 추적 상태로 전환
            controller.TransactionToState(MonsterFSMController.STATE.DETECT);
            return;
        }

        // 배회할 이동 위치가 존재한다면
        if (targetTransform != null)
        {
            // 배회할 위치 근처에 도달했다면
            targetDistance = Vector3.Distance(transform.position, targetPosition);
            if (targetDistance < 1f)
            {
                // 대기 상태로 전환
                controller.TransactionToState(MonsterFSMController.STATE.IDLE);
            }
        }
    }

    // 배회 상태 종료
    public override void ExitState()
    {
        // 네비게이션 이동 종료
        navMeshAgent.isStopped = true;

        // 배회 관련 위치 정보들 초기화
        targetTransform = null;
        targetPosition = Vector3.positiveInfinity;
        targetDistance = Mathf.Infinity;
    }

    // 새로운 배회 위치를 탐색함
    protected virtual void NewRandomDestination(bool retry)
    {
        // 배회 위치 인덱스 추첨
        int index = Random.Range(0, fsmInfo.WanderPoints.Length);

        // 같은 배회 위치를 탐색 했다면 다시 탐색
        float distance = Vector3.Distance(fsmInfo.WanderPoints[index].position, transform.position);
        if (distance < fsmInfo.NextPointSelectDistance && retry)
        {
            // 배회할 위치를 다시 추첨함
            NewRandomDestination(true);
            return;
        }

        // 배회 위치로 선정
        targetTransform = fsmInfo.WanderPoints[index];

        // 배회 위치를 기준으로한 일정 범위안의 랜덤한 위치를 재선정
        Vector3 randomDirection = Random.insideUnitSphere * fsmInfo.NextPointSelectDistance;
        randomDirection += fsmInfo.WanderPoints[index].position;
        randomDirection.y = 0f;

        // 랜덤 추첨한 배회 위치를 네비게이션 에이전트 이동 속도로 설정
        targetPosition = randomDirection;

        Debug.Log($"배회/복귀 이동할 위치 : {targetPosition}");

        // 네비게이션 이동이 유효하다면
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, fsmInfo.WanderNavCheckRadius, 1))
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.speed = fsmInfo.WanderMoveSpeed;
            navMeshAgent.SetDestination(targetPosition);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 몬스터 배회 상태
public class MonsterWanderState : MonsterRoamingState
{
    public override void EnterState(MonsterFSMController.STATE state)
    {
        navMeshAgent.speed = fsmInfo.WanderMoveSpeed;

        base.EnterState(state);

        // 새로운 배회 위치를 탐색
        NewRandomDestination(true);
    }
}

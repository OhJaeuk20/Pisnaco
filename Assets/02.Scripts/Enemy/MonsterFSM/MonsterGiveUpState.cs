using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 몬스터 추적 포기 상태
public class MonsterGiveUpState : MonsterRoamingState
{
    public override void EnterState(MonsterFSMController.STATE state)
    {
        navMeshAgent.speed = fsmInfo.GiveUpMoveSpeed;

        base.EnterState(state);

        NewRandomDestination(false);
    }
}

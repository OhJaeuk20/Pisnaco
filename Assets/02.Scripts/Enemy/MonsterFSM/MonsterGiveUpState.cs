using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ���� ���� ����
public class MonsterGiveUpState : MonsterRoamingState
{
    public override void EnterState(MonsterFSMController.STATE state)
    {
        navMeshAgent.speed = fsmInfo.GiveUpMoveSpeed;

        base.EnterState(state);

        NewRandomDestination(false);
    }
}

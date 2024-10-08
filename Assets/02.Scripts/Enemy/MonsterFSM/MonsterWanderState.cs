using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// ���� ��ȸ ����
public class MonsterWanderState : MonsterRoamingState
{
    public override void EnterState(MonsterFSMController.STATE state)
    {
        navMeshAgent.speed = fsmInfo.WanderMoveSpeed;

        base.EnterState(state);

        // ���ο� ��ȸ ��ġ�� Ž��
        NewRandomDestination(true);
    }
}

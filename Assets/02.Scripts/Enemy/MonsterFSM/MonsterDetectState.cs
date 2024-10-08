using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ���� ����
public class MonsterDetectState : MonsterState
{
	// ���� ���� ����
	public override void EnterState(MonsterFSMController.STATE state)
	{
        // ���� �ӵ� ����
        navMeshAgent.speed = fsmInfo.DetectMoveSpeed;
        // ���� �ִϸ��̼� ���
        animator.SetInteger("State", (int)state);
    }

	// ���� ���� ����
	public override void UpdateState()
	{
        // �����ϴ� ���� ����� ���� ���� �Ÿ������� ���Դٸ�
        if (controller.GetPlayerDistance() <= fsmInfo.AttackDistance)
        {
            // ���� ���·� ��ȯ��
            controller.TransactionToState(MonsterFSMController.STATE.ATTACK);
            return;
        }

        // ���� ����� ���� ���ɰŸ��� �Ѿ �������ٸ�
        if (controller.GetPlayerDistance() > fsmInfo.DetectDistance)
        {
            // ����(��ȸ ��ġ)�� ������
            controller.TransactionToState(MonsterFSMController.STATE.GIVEUP);
            return;
        }

        // ���� ��� ���� ó��
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(controller.Player.transform.position);
    }

	// ���� ���� ����
	public override void ExitState()
	{
        animator.speed = 1; // (�ӽ�) �ִϸ��̼� ��� �ӵ� ����
    }
}

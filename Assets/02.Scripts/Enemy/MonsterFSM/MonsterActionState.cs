using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ���� ����
public class MonsterActionState : MonsterState
{
    [SerializeField] private GameObject fxPrefab;
    public bool aniEnd = false;

    // ���� ���� ����
    public override void EnterState(MonsterFSMController.STATE state)
	{
        base.EnterState(state);
        // ������ ���� �̵� ����
        NavigationStop();

        // ���� ���� �ִϸ��̼� ���
        animator.SetInteger("State", (int)state);
    }

	// ���� ���� ����
	public override void UpdateState()
	{
        if (!aniEnd) return;
        
        // �°� �ִ� ���°� �ƴϰ� ���ݰ����� ���¸�
        if (controller.GetPlayerDistance() <= fsmInfo.AttackDistance)
        {
            // ���� ���·� ��ȯ
            controller.TransactionToState(MonsterFSMController.STATE.ATTACK);
            return;
        }

        // �÷��̾���� �Ÿ��� �����ؾ��� �Ÿ���
        if (controller.GetPlayerDistance() <= fsmInfo.DetectDistance)
        {
            // �������·� ��ȯ
            controller.TransactionToState(MonsterFSMController.STATE.DETECT);
            return;
        }
    }

	// ���� ���� ����
	public override void ExitState()
	{
        aniEnd = false;
    }

    
}

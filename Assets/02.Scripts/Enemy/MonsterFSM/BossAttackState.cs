using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ���� ����
public class BossAttackState : MonsterState
{
    public bool dontTurn= false;

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
        if (dontTurn) return; // ���� ����� �ֽ���
        LookAtTarget();

        if (skillController.isCasting == true) return;
        // ���� ����� ���� ���� �Ÿ����� �־����ٸ�
        if (controller.GetPlayerDistance() > fsmInfo.AttackDistance)
        {
            // ��ȸ ��ġ�� ����
            controller.TransactionToState(MonsterFSMController.STATE.DETECT);
            return;
        }
    }

	// ���� ���� ����
	public override void ExitState()
	{

	}

    public void ToggleDontTurn()
    {
        dontTurn = !dontTurn;
    }

    protected void LookAtTarget()
    {
        // ���� ����� ���� ������ ���
        Vector3 direction = (controller.Player.transform.position - transform.position).normalized;

        // ȸ�� ���ʹϾ� ���
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));

        // ���� ȸ��
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * fsmInfo.LookAtMaxSpeed);
    }
}

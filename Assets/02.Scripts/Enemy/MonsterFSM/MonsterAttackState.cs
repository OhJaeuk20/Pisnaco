using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ���� ����
public class MonsterAttackState : MonsterState
{
	// ���� ���� ����
	public override void EnterState(MonsterFSMController.STATE state)
	{
        base.EnterState(state);

        // ������ ���� �̵� ����
        NavigationStop();

        // ���� ���� �ִϸ��̼� ���
        animator.SetInteger("State", (int)state);
    }

    private bool isAttack = false;

	// ���� ���� ����
	public override void UpdateState()
	{
        // ���� ����� �ֽ���
        LookAtTarget();

        // ���� ����� ���� ���� �Ÿ����� �־����ٸ�
        if ( !isAttack && (controller.GetPlayerDistance() > fsmInfo.AttackDistance))
        {
            // ��ȸ ��ġ�� ����
            controller.TransactionToState(MonsterFSMController.STATE.GIVEUP);
            return;
        }
        else
        {
            isAttack = true;
        }
    }

	// ���� ���� ����
	public override void ExitState()
	{

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

    public void IsAttackFalse()
    {
        isAttack = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ���� ����
public class BossActionState : MonsterState
{
    public bool aniEnd = false;
    private BossHealth health;

    void Start()
    {
        health = GetComponent<BossHealth>();
    }

    // ���� ���� ����
    public override void EnterState(MonsterFSMController.STATE state)
	{
        base.EnterState(state);
        // ������ ���� �̵� ����
        NavigationStop();

        // ���� ���� �ִϸ��̼� ���
        animator.SetInteger("Phase", (int)health.currentPhase);
        animator.SetInteger("State", (int)state);
        animator.SetTrigger("Action");
    }

	// ���� ���� ����
	public override void UpdateState()
	{
        LookAtTarget();
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

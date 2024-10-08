using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// ���� ��ȸ ����
public class MonsterRoamingState : MonsterState
{
    // ��ȸ ��ġ ���ӿ�����Ʈ ����
    protected Transform targetTransform = null;
    // ��ȸ ��ġ (�⺻ : ���� ��ġ��)
    public Vector3 targetPosition = Vector3.positiveInfinity;
    public float targetDistance = Mathf.Infinity;

    // ��ȸ ���� ����
    public override void EnterState(MonsterFSMController.STATE state)
    {
        base.EnterState(state);

        // ��ȸ �ִϸ��̼� ���
        animator.SetInteger("State", (int)state);
    }

    // ��ȸ ���� ����
    public override void UpdateState()
    {
        // �÷��̾ ���� ���� �Ÿ��ȿ� ������
        if (controller.GetPlayerDistance() <= fsmInfo.AttackDistance)
        {
            // ���� ���·� ��ȯ
            controller.TransactionToState(MonsterFSMController.STATE.ATTACK);
            return;
        }

        // �÷��̾ ���� ���� �Ÿ��ȿ� ������
        if (controller.GetPlayerDistance() <= fsmInfo.DetectDistance)
        {
            // ���� ���·� ��ȯ
            controller.TransactionToState(MonsterFSMController.STATE.DETECT);
            return;
        }

        // ��ȸ�� �̵� ��ġ�� �����Ѵٸ�
        if (targetTransform != null)
        {
            // ��ȸ�� ��ġ ��ó�� �����ߴٸ�
            targetDistance = Vector3.Distance(transform.position, targetPosition);
            if (targetDistance < 1f)
            {
                // ��� ���·� ��ȯ
                controller.TransactionToState(MonsterFSMController.STATE.IDLE);
            }
        }
    }

    // ��ȸ ���� ����
    public override void ExitState()
    {
        // �׺���̼� �̵� ����
        navMeshAgent.isStopped = true;

        // ��ȸ ���� ��ġ ������ �ʱ�ȭ
        targetTransform = null;
        targetPosition = Vector3.positiveInfinity;
        targetDistance = Mathf.Infinity;
    }

    // ���ο� ��ȸ ��ġ�� Ž����
    protected virtual void NewRandomDestination(bool retry)
    {
        // ��ȸ ��ġ �ε��� ��÷
        int index = Random.Range(0, fsmInfo.WanderPoints.Length);

        // ���� ��ȸ ��ġ�� Ž�� �ߴٸ� �ٽ� Ž��
        float distance = Vector3.Distance(fsmInfo.WanderPoints[index].position, transform.position);
        if (distance < fsmInfo.NextPointSelectDistance && retry)
        {
            // ��ȸ�� ��ġ�� �ٽ� ��÷��
            NewRandomDestination(true);
            return;
        }

        // ��ȸ ��ġ�� ����
        targetTransform = fsmInfo.WanderPoints[index];

        // ��ȸ ��ġ�� ���������� ���� �������� ������ ��ġ�� �缱��
        Vector3 randomDirection = Random.insideUnitSphere * fsmInfo.NextPointSelectDistance;
        randomDirection += fsmInfo.WanderPoints[index].position;
        randomDirection.y = 0f;

        // ���� ��÷�� ��ȸ ��ġ�� �׺���̼� ������Ʈ �̵� �ӵ��� ����
        targetPosition = randomDirection;

        Debug.Log($"��ȸ/���� �̵��� ��ġ : {targetPosition}");

        // �׺���̼� �̵��� ��ȿ�ϴٸ�
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, fsmInfo.WanderNavCheckRadius, 1))
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.speed = fsmInfo.WanderMoveSpeed;
            navMeshAgent.SetDestination(targetPosition);
        }
    }
}

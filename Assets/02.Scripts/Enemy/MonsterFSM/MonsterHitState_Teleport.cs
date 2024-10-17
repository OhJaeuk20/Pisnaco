using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �ǰ� ����
public class MonsterHitState_Teleport : MonsterState
{
	// �ǰ� ����
    [SerializeField] private bool isHit;
    public bool IsHit { get => isHit; set => isHit = value; }
    private bool isTeleported;

	// �ǰ� ��ƼŬ ������Ʈ
	[SerializeField] protected ParticleSystem hitFx;
    [SerializeField] protected ParticleSystem teleFx;

    // ü�� ������Ʈ
    private MonsterHealth health;

    // �ǰ� �˹� �ð�
    [SerializeField] protected float knockbackTime;
    // �ǰ� �˹� ��
    [SerializeField] protected float knockbackForce;

    protected override void Awake()
    {
        base.Awake();

        health = GetComponent<MonsterHealth>();
    }

    //  �ǰ� ���� ����
    public override void EnterState(MonsterFSMController.STATE state)
	{
        base.EnterState(state);

        // �̵� ����
        navMeshAgent.isStopped = true;

        // �ǰ� ȿ�� ó��
        hitFx.Play();

        // �ǰ� �ִϸ��̼� ���
        animator.SetInteger("State", (int)state);

        // �ǰ� �˹� ó�� �ڷ�ƾ ����
        StartCoroutine(ApplyHitKnockback(-transform.forward));
    }

	// �ǰ� ���� ����
	public override void UpdateState()
	{
        // �̹� �ǰ��� ���� ���̸� �о�
        if (health.IsHit)
        {
            return;
        }

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

	// �ǰ� ���� ����
	public override void ExitState()
	{
        health.IsHit = false;
    }

    // �˹� ó�� �ڷ�ƾ
    private IEnumerator ApplyHitKnockback(Vector3 hitDirection)
    {
        // �ǰ� ���� ���
        health.IsHit = true;
        // �׺���̼� ����
        navMeshAgent.isStopped = true;

        // �˹� �̵� ó�� ����
        float timer = 0f;
        while (timer < knockbackTime)
        {
            navMeshAgent.Move(hitDirection * knockbackForce * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }

        Teleport();

        // �׺���̼� �簡��
        navMeshAgent.isStopped = false;
        // �ǰ� ���� ����
        health.IsHit = false;

        yield return new WaitForSeconds(knockbackTime);
        isTeleported = false;
    }

    public void Teleport()
    {
        if (isTeleported) return;

        Instantiate(teleFx, transform.position, transform.rotation);
        if (fsmInfo.WanderPoints.Length > 0)
        {
            int index = Random.Range(0, fsmInfo.WanderPoints.Length);
            Transform tel_point = fsmInfo.WanderPoints[index];
            navMeshAgent.Warp(tel_point.position);
            Instantiate(teleFx, transform.position, transform.rotation);

            isTeleported = true;
        }
    }
}

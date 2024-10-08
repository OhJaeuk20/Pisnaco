using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ��� ����
public class MonsterDeathState : MonsterState
{
    // ��� �Ϸ� ó�� �ð�
    protected float time;

    [SerializeField] protected float deathDelayTime;

    // �ǰ� ��ƼŬ ������Ʈ
    [SerializeField] protected ParticleSystem hitParticle;

    // ��� ó�� ����Ʈ
    [SerializeField] protected GameObject destroyParticlePrefab;

    [SerializeField] protected Transform destroyParticleTr;

    // �ǰ� �˹� �ð�
    [SerializeField] protected float knockbackTime;
    // �ǰ� �˹� ��
    [SerializeField] protected float knockbackForce;

    //  ��� ���� ����
    public override void EnterState(MonsterFSMController.STATE state)
	{
        base.EnterState(state);

        // �ǰ� ȿ�� ó��
        hitParticle.Play();

        // ��� �ִϸ��̼� ���
        animator.SetBool("Dead", true);

        // �ǰ� �˹� ó�� �ڷ�ƾ ����
        StartCoroutine(ApplyDeathKnockback(-transform.forward));
    }

	// ��� ���� ����
	public override void UpdateState()
	{
        time += Time.deltaTime;

        // ��� ó�� �����ð��� �����ٸ�
        if (time >= deathDelayTime)
        {
            ExitState();
        }
    }

	// ��� ���� ����
	public override void ExitState()
	{
        // ���Ͱ� �Ҹ��
        Instantiate(destroyParticlePrefab, destroyParticleTr.position, Quaternion.identity);
        Destroy(gameObject);
    }

    // �˹� ó�� �ڷ�ƾ
    private IEnumerator ApplyDeathKnockback(Vector3 hitDirection)
    {
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
    }
}

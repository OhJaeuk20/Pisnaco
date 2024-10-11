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

    protected Transform destroyParticleTr;

    // �ǰ� �˹� �ð�
    [SerializeField] protected float knockbackTime;
    // �ǰ� �˹� ��
    [SerializeField] protected float knockbackForce;

    // ��ü ����ɴ� �ӵ�
    [SerializeField] protected float sinkSpeed;

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

    public void StartSinkingCoroutine()
    {
        navMeshAgent.enabled = false;
        Instantiate(destroyParticlePrefab, transform.position, Quaternion.identity);
        StartCoroutine(SinkCoroutine());
    }

    private IEnumerator SinkCoroutine()
    {
        float sinkDuration = 5f;
        float elapsedTime = 0f;

        while (elapsedTime < sinkDuration)
        {
            // y������ õõ�� �������
            transform.position -= new Vector3(0, sinkSpeed * Time.deltaTime, 0);
            elapsedTime += Time.deltaTime;
            yield return null;  // �� ������ ��ٸ�
        }
    }
}

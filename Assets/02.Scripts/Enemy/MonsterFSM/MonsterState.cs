using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class MonsterState : MonoBehaviour
{
	// ���� ���ѻ��±�� ��Ʈ�ѷ�
	protected MonsterFSMController controller;

	// �ִϸ����� ������Ʈ
	protected Animator animator;

	// �׺���̼� ������Ʈ
	protected NavMeshAgent navMeshAgent;

    protected MonsterFSMInfo fsmInfo; // ���� ����

	protected MonsterSkillController skillController;

	[Range(0f, 2f)]
	[SerializeField] protected float animSpeed;

    protected virtual void Awake()
	{
		controller = GetComponent<MonsterFSMController>();
		animator = GetComponent<Animator>();
		navMeshAgent = GetComponent<NavMeshAgent>();
        fsmInfo = GetComponent<MonsterFSMInfo>();
		skillController = GetComponent<MonsterSkillController>();
    }

	// ���� ���� ���� �������̽�(�����������̽��ƴ�) �޼ҵ� ����

	// ���� ���� ����(�ٸ����¿��� ���̵�) �޼ҵ�
	public virtual void EnterState(MonsterFSMController.STATE state)
	{
        animator.speed = animSpeed; // (�ӽ�) �ִϸ��̼� �ӵ� ����
    }

	// ���� ���� ������Ʈ �߻� �޼ҵ� (���� ���� ����)
	public abstract void UpdateState();

	// ���� ���� ����(�ٸ����·� ���̵�) �޼ҵ�
	public abstract void ExitState();

	protected virtual void NavigationStop()
	{
		navMeshAgent.isStopped = true;
		navMeshAgent.speed = 0f;
	}
}

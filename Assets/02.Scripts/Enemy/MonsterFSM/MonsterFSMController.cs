using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using System.Linq;

// ���� FSM ��Ʈ�ѷ�
public class MonsterFSMController : MonoBehaviour
{
    // ���� ���µ�
    public enum STATE { IDLE, WANDER, DETECT, ATTACK, GIVEUP, HIT, DEATH, ACTION }

    // ������ ���� ���� ����
    [SerializeField] private MonsterState currentState;

    // ������ ��� ���µ�
    [SerializeField] private MonsterState[] monsterStates;

    // �÷��̾� ����
    private GameObject player;
	public GameObject Player { get => player; set => player = value; }

	private MonsterSkillController skillController;

	// Start is called before the first frame update
	void Start()
	{
		Player = GameObject.FindWithTag("Player");

		if (GetComponent<MonsterSkillController>() != null)
			skillController = GetComponent<MonsterSkillController>();

		// ��� ���·� ����
		TransactionToState(STATE.IDLE);
	}

	// Update is called once per frame
	void Update()
	{
        // * ���� ������ ������ ����� ����!
        currentState?.UpdateState();
	}

	// * ���� ��ȯ �޼ҵ�
	public void TransactionToState(STATE state)
	{
		Debug.Log($"���� ���� ��ȯ : {state}");

		// ���� ���Ͱ� �̹� ��� ���¸� ���� ��ȯ�� ���� ����
		if (currentState == monsterStates[(int)STATE.DEATH]) return;

		currentState?.ExitState(); // ���� ���� ����
		currentState = monsterStates[(int)state]; // ���� ��ȯ ó��
		currentState.EnterState(state); // ���ο� ���� ����
	}

	// ���� ��Ʈ�ѷ� ��ɵ�

	// �÷��̾�� ���Ͱ��� �Ÿ� ����
	public float GetPlayerDistance()
	{
		return Vector3.Distance(transform.position, Player.transform.position);
	}

	public int CurrentStateToInt()
	{
		int stateNum = 0;
		foreach (MonsterState curState in monsterStates)
		{
			if (curState == currentState)
			{
				break;
			}
			stateNum++;
		}
		return stateNum;
	}
}

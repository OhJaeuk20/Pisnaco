using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using System.Linq;

// 몬스터 FSM 컨트롤러
public class MonsterFSMController : MonoBehaviour
{
    // 몬스터 상태들
    public enum STATE { IDLE, WANDER, DETECT, ATTACK, GIVEUP, HIT, DEATH, ACTION }

    // 몬스터의 현재 동작 상태
    [SerializeField] private MonsterState currentState;

    // 몬스터의 모든 상태들
    [SerializeField] private MonsterState[] monsterStates;

    // 플레이어 참조
    private GameObject player;
	public GameObject Player { get => player; set => player = value; }

	private MonsterSkillController skillController;

	// Start is called before the first frame update
	void Start()
	{
		Player = GameObject.FindWithTag("Player");

		if (GetComponent<MonsterSkillController>() != null)
			skillController = GetComponent<MonsterSkillController>();

		// 대기 상태로 시작
		TransactionToState(STATE.IDLE);
	}

	// Update is called once per frame
	void Update()
	{
        // * 현재 설정된 상태의 기능을 동작!
        currentState?.UpdateState();
	}

	// * 상태 전환 메소드
	public void TransactionToState(STATE state)
	{
		Debug.Log($"몬스터 상태 전환 : {state}");

		// 현재 몬스터가 이미 사망 상태면 상태 전환을 하지 않음
		if (currentState == monsterStates[(int)STATE.DEATH]) return;

		currentState?.ExitState(); // 이전 상태 정리
		currentState = monsterStates[(int)state]; // 상태 전환 처리
		currentState.EnterState(state); // 새로운 상태 전이
	}

	// 보조 컨트롤러 기능들

	// 플레이어와 몬스터간의 거리 측정
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

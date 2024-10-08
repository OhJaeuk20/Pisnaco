using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 몬스터 유한 상태 관련 정보
public class MonsterFSMInfo : MonoBehaviour
{
    [Header("공격 관련 속성")]
    [SerializeField] private float attackDistance; // 공격 가능 거리
	[SerializeField] private float attackSpeed; // 공격 속도
    [SerializeField] private float lookAtMaxSpeed;

    [Header("추적 관련 속성")]
	[SerializeField] private float detectDistance; // 초적 가능 거리
	[SerializeField] private float detectMoveSpeed; // 추적 시 이동 속도

    [Header("로밍 관련 속성")]
    [SerializeField] private float nextPointSelectDistance; // 다음 이동 선택 제한 거리
    [SerializeField] private Transform[] wanderPoints;

    [Header("배회 관련 속성")]
    [SerializeField] private float wanderMoveSpeed; // 배회 시 이동 속도
	[SerializeField] private float wanderNavCheckRadius; // 배회 이동 네비게이션 체크 영역

    [Header("퇴각 관련 속성")]
	[SerializeField] private float giveUpMoveSpeed; // 퇴각 이동 속도

    public Transform[] WanderPoints { get => wanderPoints; set => wanderPoints = value; }

	public float AttackDistance { get => attackDistance; set => attackDistance = value; }
	public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
	public float DetectDistance { get => detectDistance; set => detectDistance = value; }
	public float DetectMoveSpeed { get => detectMoveSpeed; set => detectMoveSpeed = value; }
	public float WanderMoveSpeed { get => wanderMoveSpeed; set => wanderMoveSpeed = value; }
	public float WanderNavCheckRadius { get => wanderNavCheckRadius; set => wanderNavCheckRadius = value; }
	public float GiveUpMoveSpeed { get => giveUpMoveSpeed; set => giveUpMoveSpeed = value; }
    public float NextPointSelectDistance { get => nextPointSelectDistance; set => nextPointSelectDistance = value; }
    public float LookAtMaxSpeed { get => lookAtMaxSpeed; set => lookAtMaxSpeed = value; }
}

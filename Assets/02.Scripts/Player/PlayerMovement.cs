using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private PlayerStat stat;
    private Animator animator;
    private PlayerAttack attack;

    // 넉다운 방향, 힘
    [SerializeField] private float knockBackForce;
    private Vector3 knockDownDir = Vector3.zero;
    public float stunDuration;
    public float currentStunTime;

    // 이동, 회전 속도
    public Vector3 moveDirection;
    public Vector3 diveDirection;

    // 중력 관련 속성
    [SerializeField] private float gravity; // 중력 수치
    public bool grounded = false;
    float vSpeed = 0.0f;

    private bool isDiveRoll;
    public bool IsDiveRoll { get => isDiveRoll; set => isDiveRoll = value; } 

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        stat = GetComponent<PlayerStat>();
        attack = GetComponent<PlayerAttack>();
    }

    void Update()
    {
        GravityDown();

        if (stat.isKnockDown) KnockDownTimer();

        if (IsDiveRoll || attack.IsPlayAttackAnimation() || stat.isKnockDown) return;

        Move();
    }

    public void DiveRollMove()
    {
        Vector3 diveMovement = diveDirection * (stat.speed * Time.deltaTime);
        characterController.Move(diveMovement);
    }

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // 입력값에 따라 이동 방향 결정
        moveDirection = new Vector3(h, 0f, v).normalized;

        if (Input.GetKeyDown(KeyCode.Space) && !IsDiveRoll && (moveDirection.magnitude > 0.1f))
        {
            animator.SetTrigger("Dodge");
            diveDirection = moveDirection.normalized;
            transform.LookAt(transform.position + moveDirection.normalized);
            return;
        }

        animator.SetFloat("Move", moveDirection.magnitude);

        // 캐릭터 회전 처리
        Vector3 targetDirection = moveDirection.normalized;
        if (targetDirection != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * stat.rotateSpeed);  // 이동 방향으로 캐릭터 회전
        }
        characterController.Move(moveDirection * (stat.speed) * Time.deltaTime);
    }

    private void GravityDown()
    {
        vSpeed = vSpeed - gravity * Time.deltaTime;

        if (vSpeed < -gravity)
            vSpeed = -gravity;

        var verticalMove = new Vector3(0, vSpeed * Time.deltaTime, 0);

        var flag = characterController.Move(verticalMove);
        if ((flag & CollisionFlags.Below) != 0)
        {
            vSpeed = 0;
        }
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("KNOCKBACKDAMAGE1"))
        {
            currentStunTime = 0.0f;
            knockDownDir = (transform.position - coll.transform.position).normalized;
            transform.LookAt(transform.position - knockDownDir);
            animator.SetTrigger("KnockDown");
        }
    }

    public void KnockDownMove()
    {
        characterController.Move((knockDownDir + transform.up).normalized * knockBackForce * Time.deltaTime);
    }

    public void KnockDownTimer()
    {
        currentStunTime += Time.deltaTime;
        if (currentStunTime >= stunDuration)
        {
            animator.SetTrigger("KnockDownEnd");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackInput : MonoBehaviour
{
    private WaitForSeconds attackInputWait;
    private Coroutine attackWaitCoroutine;
    [SerializeField] private float attackInputDuration;

    public bool isAttackInput;

    private Animator animator;

    private PWeapon weapon;

    // 공격 애니메이션 해시값
    private int hashMeleeAttack = Animator.StringToHash("MeleeAttack");
    private int hashStateTime = Animator.StringToHash("StateTime");
    private int hashCombo1 = Animator.StringToHash("Combo1");
    private int hashCombo2 = Animator.StringToHash("Combo2");
    private int hashCombo3 = Animator.StringToHash("Combo3");

    // 애니메이션 상태
    private AnimatorStateInfo currentStateInfo;

    void Start()
    {
        weapon = GetComponentInChildren<PWeapon>();
        animator = GetComponent<Animator>();
        attackInputWait = new WaitForSeconds(attackInputDuration);
    }

    void Update()
    {
        if (PlayerStat.playerDie) return;
        // 공격 입력 지연 처리
        if (Input.GetMouseButtonDown(0) && !isAttackInput)
        {
            if (attackWaitCoroutine != null)
                StopCoroutine(attackWaitCoroutine);
            LookClickPoint();
            attackWaitCoroutine = StartCoroutine(AttackWait());
        }

        // 애니메이션 레이어 진행 상태(퍼센트) 정보를 애니메이션 상태 시간으로 반복 설정
        animator.SetFloat(hashStateTime, Mathf.Repeat(animator.GetCurrentAnimatorStateInfo(0).normalizedTime, 1f));
        // 공격 Trigger 리셋
        animator.ResetTrigger(hashMeleeAttack);

        // 공격 애니메이션 재생
        if (isAttackInput)
        {
            animator.SetTrigger(hashMeleeAttack);
        }
    }

    IEnumerator AttackWait()
    {
        isAttackInput = true; // 공격 입력 가능

        yield return attackInputWait; // 공격 입력 지연 대기

        isAttackInput = false; // 공격 입력 불가능
    }

    public bool IsPlayAttackAnimation()
    {
        currentStateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (currentStateInfo.shortNameHash == hashMeleeAttack ||
            currentStateInfo.shortNameHash == hashCombo1 ||
            currentStateInfo.shortNameHash == hashCombo2 ||
            currentStateInfo.shortNameHash == hashCombo3)
        {
            return true;
        }

        return false;
    }
    
    private void LookClickPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // 2. 캐릭터가 마우스 클릭한 위치를 바라보도록 회전
            Vector3 targetPosition = hit.point;  // 클릭한 지점의 월드 좌표
            targetPosition.y = transform.position.y;  // 캐릭터의 y 축은 유지 (수평 회전)

            // 3. 바라보는 방향 계산 (목표 지점 - 현재 위치)
            Vector3 direction = (targetPosition - transform.position).normalized;

            // 4. 즉시 회전
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}

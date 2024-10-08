using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
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
}

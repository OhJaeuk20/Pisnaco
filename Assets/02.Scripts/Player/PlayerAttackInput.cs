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

    // ���� �ִϸ��̼� �ؽð�
    private int hashMeleeAttack = Animator.StringToHash("MeleeAttack");
    private int hashStateTime = Animator.StringToHash("StateTime");
    private int hashCombo1 = Animator.StringToHash("Combo1");
    private int hashCombo2 = Animator.StringToHash("Combo2");
    private int hashCombo3 = Animator.StringToHash("Combo3");

    // �ִϸ��̼� ����
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
        // ���� �Է� ���� ó��
        if (Input.GetMouseButtonDown(0) && !isAttackInput)
        {
            if (attackWaitCoroutine != null)
                StopCoroutine(attackWaitCoroutine);
            LookClickPoint();
            attackWaitCoroutine = StartCoroutine(AttackWait());
        }

        // �ִϸ��̼� ���̾� ���� ����(�ۼ�Ʈ) ������ �ִϸ��̼� ���� �ð����� �ݺ� ����
        animator.SetFloat(hashStateTime, Mathf.Repeat(animator.GetCurrentAnimatorStateInfo(0).normalizedTime, 1f));
        // ���� Trigger ����
        animator.ResetTrigger(hashMeleeAttack);

        // ���� �ִϸ��̼� ���
        if (isAttackInput)
        {
            animator.SetTrigger(hashMeleeAttack);
        }
    }

    IEnumerator AttackWait()
    {
        isAttackInput = true; // ���� �Է� ����

        yield return attackInputWait; // ���� �Է� ���� ���

        isAttackInput = false; // ���� �Է� �Ұ���
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
            // 2. ĳ���Ͱ� ���콺 Ŭ���� ��ġ�� �ٶ󺸵��� ȸ��
            Vector3 targetPosition = hit.point;  // Ŭ���� ������ ���� ��ǥ
            targetPosition.y = transform.position.y;  // ĳ������ y ���� ���� (���� ȸ��)

            // 3. �ٶ󺸴� ���� ��� (��ǥ ���� - ���� ��ġ)
            Vector3 direction = (targetPosition - transform.position).normalized;

            // 4. ��� ȸ��
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}

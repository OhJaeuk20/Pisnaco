using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonChargeAttack : MonsterSkill
{
    [SerializeField] private float ChargeDuration;
    public BoxCollider boxCollider;
    public CapsuleCollider capsuleCollider;
    public float chargeSpeed;
    private bool isCharge = false;

    public override void PerformAttack(int skillNum)
    {
        animator.SetInteger("Skill", skillNum);        
    }

    public void StartChargeAttack()
    {
        ToggleIsCharge();
        StopCoroutine("DelayChargeEnd");
        StartCoroutine("DelayChargeEnd");
    }

    public void Charge()
    {
        
    }

    IEnumerator DelayChargeEnd()
    {
        yield return new WaitForSeconds(ChargeDuration);
        ChargeEnd();
    }

    private void ChargeEnd()
    {
        Debug.Log("���� ��");
        ToggleIsCharge();
        animator.SetInteger("Skill", -1);
        Debug.Log("ChargeEnd");
    }

    public void ToggleIsCharge()
    {
        isCharge = !isCharge;
        animator.SetBool("IsCharge", isCharge);
    }

    public void ChargeTriggerEnter(Collider coll)
    {
        if (coll.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("�÷��̾� �浹");

            // �浹�� �÷��̾��� ��ġ�� �ڽ� �ݶ��̴� �������� �������� ���������� ���
            Vector3 localHitPoint = transform.InverseTransformPoint(coll.transform.position);

            Vector3 knockBackDir; // �˹� ������ ����

            // �ڽ� �ݶ��̴��� ���� ��ǥ �������� x ��ǥ�� ����� ������, ������ ����
            if (localHitPoint.x >= 0)
            {
                // ������ �������� �˹�
                knockBackDir = transform.right; // �ڽ� �ݶ��̴��� ������ ����
                Debug.Log("���������� �˹�");
            }
            else
            {
                // ���� �������� �˹�
                knockBackDir = -transform.right; // �ڽ� �ݶ��̴��� ���� ����
                Debug.Log("�������� �˹�");
            }

            // �÷��̾� �˹� ó��
            coll.GetComponent<PlayerMovement>().KnockBack(knockBackDir);

            // �÷��̾�� 1 ������
            coll.GetComponent<PlayerStat>().Hit(1);

            // �ڽ� �ݶ��̴� ��Ȱ��ȭ
            boxCollider.enabled = false;
        }

        if (coll.CompareTag("WALL"))
        {
            StopCoroutine("DelayChargeEnd");
            ChargeEnd();
            Debug.Log("���� �浹");
        }
        if (coll.CompareTag("STUNOBJ"))
        {
            StopCoroutine("DelayChargeEnd");
            ChargeEnd();
            Debug.Log("StunObj�� �浹");
        }
    }
}
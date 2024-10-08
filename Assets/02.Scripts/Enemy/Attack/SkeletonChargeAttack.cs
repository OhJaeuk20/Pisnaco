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
        Debug.Log("돌진 끝");
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
            Debug.Log("플레이어 충돌");

            // 충돌한 플레이어의 위치가 박스 콜라이더 기준으로 왼쪽인지 오른쪽인지 계산
            Vector3 localHitPoint = transform.InverseTransformPoint(coll.transform.position);

            Vector3 knockBackDir; // 넉백 방향을 선언

            // 박스 콜라이더의 로컬 좌표 기준으로 x 좌표가 양수면 오른쪽, 음수면 왼쪽
            if (localHitPoint.x >= 0)
            {
                // 오른쪽 방향으로 넉백
                knockBackDir = transform.right; // 박스 콜라이더의 오른쪽 방향
                Debug.Log("오른쪽으로 넉백");
            }
            else
            {
                // 왼쪽 방향으로 넉백
                knockBackDir = -transform.right; // 박스 콜라이더의 왼쪽 방향
                Debug.Log("왼쪽으로 넉백");
            }

            // 플레이어 넉백 처리
            coll.GetComponent<PlayerMovement>().KnockBack(knockBackDir);

            // 플레이어에게 1 데미지
            coll.GetComponent<PlayerStat>().Hit(1);

            // 박스 콜라이더 비활성화
            boxCollider.enabled = false;
        }

        if (coll.CompareTag("WALL"))
        {
            StopCoroutine("DelayChargeEnd");
            ChargeEnd();
            Debug.Log("벽과 충돌");
        }
        if (coll.CompareTag("STUNOBJ"))
        {
            StopCoroutine("DelayChargeEnd");
            ChargeEnd();
            Debug.Log("StunObj와 충돌");
        }
    }
}
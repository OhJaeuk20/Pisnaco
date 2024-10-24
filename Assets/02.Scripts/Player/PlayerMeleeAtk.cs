using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMeleeAtk : MonoBehaviour
{
    // 공격 대상 레이어
    [SerializeField] private LayerMask targetLayer;

    // 공격 타겟 중심점 위치
    [SerializeField] private Transform attackTransform;

    // 공격 범위
    [SerializeField] private float attackRadius;

    // 공격 범위 각도
    [SerializeField] private float hitAngle;


    // 공격 애니메이션 피격 이벤트
    public void AttackHitAnimationEvent()
    {
        // * Physics.OverlapSphere(충돌체크중심점위치, 충돌체크범위, 대상레이어);
        // - 레이캐스트처럼 해당 메소드가 실행되는 순간 설정 영역안에 있는 충돌 대상들을 검출함
        Collider[] hits = Physics.OverlapSphere(attackTransform.position, attackRadius, targetLayer);

        // 피격된 대상들 중  지정된 각도 안에있는 대상을 타격함
        foreach (Collider hit in hits)
        {
            // 플레이어가 타격을 향한 방향벡터를 구함
            Vector3 directionToTarget = hit.transform.position - transform.position;

            // 타격 대상과의 시선 각도를 구함
            float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);

            if (angleToTarget < hitAngle)
            {
                Debug.Log($"{hit.name} 몬스터를 타격함");
                if(hit.GetComponent<MonsterHealth>() != null)
                {
                    // 몬스터 피격 처리
                    hit.GetComponent<MonsterHealth>().Hit(1);
                }
                else if(hit.GetComponent<BossHealth>() != null)
                {
                    hit.GetComponent<BossHealth>().Hit(1);
                }
                else if(hit.gameObject.tag == "PROJECTILE")
                {
                    Destroy(hit.gameObject);
                }
            }
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawSphere(attackTransform.position, attackRadius);
    //}
}

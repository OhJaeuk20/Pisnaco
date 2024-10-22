using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEAtk : MonoBehaviour
{
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask targetLayer;

    void Start()
    {
        DetectPlayer();
    }

    public void DetectPlayer()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRadius, targetLayer);

        // 피격된 대상들 중  지정된 각도 안에있는 대상을 타격함
        foreach (Collider hit in hits)
        {
            Debug.Log($"{hit.name} 플레이어를 타격함");

            // 플레이어 피격 처리
            hit.GetComponent<PlayerStat>().Hit(1);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, attackRadius);
    }
}
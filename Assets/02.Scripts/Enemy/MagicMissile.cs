using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;              // 미사일 이동 속도
    [SerializeField] private float rotationSpeed = 200f;     // 미사일 회전 속도
    [SerializeField] private float lifeTime = 10.0f;         // 미사일 유지 시간
    [SerializeField] private int damage = 20;                // 미사일이 플레이어에게 가하는 데미지
    [SerializeField] private LayerMask targetLayer;          // 탐지할 레이어 (Player 등의 타겟)

    private Transform targetTransform;                       // 타겟의 Transform
    private Rigidbody rb;                                    // 미사일의 Rigidbody

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // 일정 시간이 지나면 미사일 파괴
        Destroy(gameObject, lifeTime);

        // 타겟 탐색
        FindTarget();
    }

    void Update()
    {
        if (targetTransform != null)
        {
            // 타겟을 향해 유도
            Vector3 directionToTarget = (targetTransform.position - transform.position).normalized;
            Vector3 rotateAmount = Vector3.Cross(transform.forward, directionToTarget);

            // 회전 속도 조절
            rb.angularVelocity = rotateAmount * rotationSpeed * Time.deltaTime;
            rb.velocity = transform.forward * speed;
        }
        else
        {
            // 타겟이 없으면 앞으로만 이동
            rb.velocity = transform.forward * speed;
        }
    }

    void FindTarget()
    {
        // OverlapSphere로 타겟 탐색
        Collider[] hits = Physics.OverlapSphere(transform.position, 50f, targetLayer);

        if (hits.Length > 0)
        {
            targetTransform = hits[0].transform; // 가장 가까운 타겟으로 설정
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // 미사일이 플레이어와 충돌했을 때 데미지 주기
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerStat playerStat = other.GetComponent<PlayerStat>();
            Debug.Log("Fire");
            if (playerStat != null)
            {
                playerStat.Hit(damage); // 플레이어에게 데미지 입히기
            }

            // 충돌 후 미사일 파괴
            Destroy(gameObject);
        }
    }

    // 디버그용: 탐지 반경 시각화
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 50f);
    }
}
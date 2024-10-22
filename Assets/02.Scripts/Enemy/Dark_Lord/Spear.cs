using System.Collections;
using UnityEngine;

public class Spear : MonoBehaviour
{
    public float speed = 10f; // 창의 속도
    private Transform target; // 플레이어 또는 목표
    private Rigidbody rb;
    public GameObject explosionPrefab; // 폭발 프리팹
    public Transform exPoint;
    public string groundLayerName; // Ground 레이어 이름
    public float explosionDelay; // 폭발 지연 시간

    void Start()
    {
        // 타겟을 찾습니다. 여기서는 플레이어로 가정
        target = GameObject.FindWithTag("Player").transform;
        if (target != null)
        {
            // 창을 플레이어 쪽으로 회전시킵니다.
            Vector3 directionToPlayer = (target.position - transform.position).normalized;
            Quaternion rotationToPlayer = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = rotationToPlayer;

            // Rigidbody를 통해 창을 목표 방향으로 날려 보냅니다.
            rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = directionToPlayer * speed;
            }
        }
    }

    // 트리거 충돌 처리
    void OnTriggerEnter(Collider other)
    {
        // Ground 레이어와 충돌했는지 확인 (이름으로 가져온 레이어와 비교)
        if (other.gameObject.layer == LayerMask.NameToLayer(groundLayerName))
        {
            rb.velocity = Vector3.zero;
            // 폭발을 지연시키는 코루틴 실행
            StartCoroutine(ExplodeAfterDelay());
        }
    }

    // 폭발 지연 코루틴
    IEnumerator ExplodeAfterDelay()
    {
        // 지연 시간만큼 대기
        yield return new WaitForSeconds(explosionDelay);

        // 폭발 처리 및 창 파괴
        Instantiate(explosionPrefab, exPoint.position, Quaternion.identity);
        Destroy(gameObject); // 창 파괴
    }
}
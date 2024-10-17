using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class AroundAOEAttack : MonsterSkill
{
    public override void PerformAttack(int skillNum)
    {
        Debug.Log("AroundAOE!!!!");
        animator.SetInteger("Skill", skillNum);
        animator.SetBool("IsChanneling", true);
    }

    public GameObject explosionPrefab;  // 폭발 프리팹
    public float explosionDistance = 2.0f;  // 폭발 사이 거리 간격
    public int explosionDirections = 8;   // 폭발 방향 개수 (8방향)
    public float delayTime;

    // 공격 애니메이션 피격 이벤트
    public void AroundAOEAtkAnimationEvent()
    {
        StartCoroutine(TriggerExplosions());
    }

    private IEnumerator TriggerExplosions()
    {
        yield return new WaitForSeconds(delayTime);  // 모든 폭발이 동시에 일어나므로 기다리지 않고 즉시 종료
        float angleStep = 360f / explosionDirections; // 각 폭발 사이의 각도 계산

        for (int j = 0; j < explosionDirections; j++)
        {
            // 각도 계산
            float angle = j * angleStep;
            // 방향 벡터 계산 (각도를 라디안으로 변환 후 방향 설정)
            Vector3 direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad));

            // 폭발 위치 계산 (시전자 기준 방향 벡터에 거리 곱해 계산)
            Vector3 explosionPosition = transform.position + direction * explosionDistance;

            // 폭발 프리팹을 생성
            Instantiate(explosionPrefab, explosionPosition, Quaternion.identity);
        }

        animator.SetBool("IsChanneling", false);
    }
}
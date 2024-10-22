using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class VanishAndBlink : MonsterSkill
{
    private Renderer[] renderers;

    [Header("Skill Settings")]
    public float disappearDuration = 1.0f;
    public float distanceBehindPlayer = 2.0f;

    [Header("Particle Effects")]
    public GameObject vanishParticlePrefab;
    public Transform paricleTr;

    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>(true);
    }

    public override void PerformAttack(int skillNum)
    {
        Debug.Log("Blink Skill Performed");
        animator.SetInteger("Skill", skillNum); // 애니메이션 시작
        animator.SetBool("IsChanneling", true);
    }

    // 공격 애니메이션에서 호출될 이벤트
    public void BlinkAnimationEvent()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
        StartCoroutine(TeleportToPlayer(target.transform));
    }

    // 텔레포트 코루틴
    IEnumerator TeleportToPlayer(Transform player)
    {
        // 플레이어 뒤의 목표 위치 계산
        Vector3 behindPlayerPosition = player.position - player.forward * distanceBehindPlayer;

        // NavMesh 상의 유효한 위치를 찾기
        if (NavMesh.SamplePosition(behindPlayerPosition, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
        {
            
            SetRenderersActive(false);
            Instantiate(vanishParticlePrefab, paricleTr.position, Quaternion.identity);

            // 2. NavMesh 상의 새 위치로 워프
            navMeshAgent.Warp(hit.position);

            // 3. 일정 시간 동안 대기
            yield return new WaitForSeconds(disappearDuration);

            // 4. 모든 렌더러를 다시 활성화 (몬스터 나타남)
            Instantiate(vanishParticlePrefab, paricleTr.position, Quaternion.identity);
            SetRenderersActive(true);
            gameObject.layer = LayerMask.NameToLayer("Monster");
            animator.SetBool("IsChanneling", false);
            Debug.Log("Monster teleported behind the player and reappeared!");
        }
        else
        {
            Debug.LogWarning("No valid NavMesh position found behind the player.");
        }
    }

    // 모든 렌더러를 활성화 또는 비활성화하는 함수
    private void SetRenderersActive(bool isActive)
    {
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = isActive;
        }
    }
}
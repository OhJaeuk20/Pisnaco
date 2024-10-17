using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class DashAttack : MonsterSkill
{
    public override void PerformAttack(int skillNum)
    {
        Debug.Log("DashAttack!!!!");
        animator.SetInteger("Skill", skillNum);
        animator.SetBool("IsChanneling", true);
    }

    public float dashDistance; // 대시 거리
    public float horizontalRange;
    public float delayTime;
    public Vector3 boxSize; // 감지 범위 크기 (X, Y, Z)

    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void StartDaggerDash()
    {
        animator.SetBool("IsChanneling", true);
        StartCoroutine(DaggerDash());
    }

    IEnumerator DaggerDash()
    {
        Vector3 start = transform.position; // 현재 위치
        Vector3 end = start + transform.forward * dashDistance; // 대시 끝점

        navMeshAgent.Warp(end);

        // 대시 시작점과 끝점의 중간 위치
        Vector3 center = (start + end) / 2;

        // OverlapBox로 적 탐지
        Collider[] hits = Physics.OverlapBox(center, boxSize, Quaternion.identity, targetLayer);

        foreach (Collider hit in hits)
        {
            hit.GetComponent<PlayerStat>().Hit(1);
        }

        yield return new WaitForSeconds(delayTime);
        animator.SetBool("IsChanneling", false);
    }
}

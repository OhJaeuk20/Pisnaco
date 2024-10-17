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

    public float dashDistance; // ��� �Ÿ�
    public float horizontalRange;
    public float delayTime;
    public Vector3 boxSize; // ���� ���� ũ�� (X, Y, Z)

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
        Vector3 start = transform.position; // ���� ��ġ
        Vector3 end = start + transform.forward * dashDistance; // ��� ����

        navMeshAgent.Warp(end);

        // ��� �������� ������ �߰� ��ġ
        Vector3 center = (start + end) / 2;

        // OverlapBox�� �� Ž��
        Collider[] hits = Physics.OverlapBox(center, boxSize, Quaternion.identity, targetLayer);

        foreach (Collider hit in hits)
        {
            hit.GetComponent<PlayerStat>().Hit(1);
        }

        yield return new WaitForSeconds(delayTime);
        animator.SetBool("IsChanneling", false);
    }
}

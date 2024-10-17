using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;

public class FrontAOEAttack : MonsterSkill
{
    public override void PerformAttack(int skillNum)
    {
        Debug.Log("AttackWithFire!!!!");
        animator.SetInteger("Skill", skillNum);
    }

    // ���� Ÿ�� �߽��� ��ġ
    [SerializeField] private Transform attackTransform;

    public GameObject explosionPrefab;  // ���� ������
    public float startDistance;
    public float explosionInterval; // ���� ���� �ð� ����
    public float explosionDistance;  // ���� ���� �Ÿ� ����
    public int explosionCount;        // ���� Ƚ��
    
    // ���� �ִϸ��̼� �ǰ� �̺�Ʈ
    public void FrontAOEAtkAnimationEvent()
    {
        // * Physics.OverlapSphere(�浹üũ�߽�����ġ, �浹üũ����, ����̾�);
        // - ����ĳ��Ʈó�� �ش� �޼ҵ尡 ����Ǵ� ���� ���� �����ȿ� �ִ� �浹 ������ ������
        Collider[] hits = Physics.OverlapSphere(attackTransform.position, attackRadius, targetLayer);

        // �ǰݵ� ���� ��  ������ ���� �ȿ��ִ� ����� Ÿ����
        foreach (Collider hit in hits)
        {
            // �÷��̾ Ÿ���� ���� ���⺤�͸� ����
            Vector3 directionToTarget = hit.transform.position - transform.position;

            // Ÿ�� ������ �ü� ������ ����
            float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);

            if (angleToTarget < hitAngle)
            {
                Debug.Log($"{hit.name} �÷��̾ Ÿ����");

                // �÷��̾� �ǰ� ó��
                hit.GetComponent<PlayerStat>().Hit(1);
            }
        }

    }

    public void AfterExplosion()
    {
        StartCoroutine(TriggerExplosions());
    }

    private IEnumerator TriggerExplosions()
    {
        Vector3 startPosition = transform.position + transform.forward * startDistance;

        for (int i = 0; i < explosionCount; i++)
        {
            // �� ������ ��ġ�� ĳ������ �������� ���� �Ÿ� ������ ��ġ
            Vector3 explosionPosition = startPosition + transform.forward * (explosionDistance * i);

            // ���� �������� �ν��Ͻ�ȭ�Ͽ� ����
            Instantiate(explosionPrefab, explosionPosition, Quaternion.identity);

            // ���� ������ ���� ���
            yield return new WaitForSeconds(explosionInterval);
        }
    }
}
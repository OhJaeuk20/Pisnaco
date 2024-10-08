using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �÷��̾� ���� ��Ʈ�� ������Ʈ
public class MonsterMeleeHit : MonoBehaviour
{
    // ���� ��� ���̾�
    [SerializeField] private LayerMask targetLayer;

    // ���� Ÿ�� �߽��� ��ġ
    [SerializeField] private Transform attackTransform;

    // ���� ����
    [SerializeField] private float attackRadius;

    // ���� ���� ����
    [SerializeField] private float hitAngle;


    // ���� �ִϸ��̼� �ǰ� �̺�Ʈ
    public void AttackPlayerAnimationEvent()
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
}

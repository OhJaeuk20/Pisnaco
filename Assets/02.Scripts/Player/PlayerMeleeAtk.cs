using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMeleeAtk : MonoBehaviour
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
    public void AttackHitAnimationEvent()
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
                Debug.Log($"{hit.name} ���͸� Ÿ����");
                if(hit.GetComponent<MonsterHealth>() != null)
                {
                    // ���� �ǰ� ó��
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

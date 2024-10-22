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

        // �ǰݵ� ���� ��  ������ ���� �ȿ��ִ� ����� Ÿ����
        foreach (Collider hit in hits)
        {
            Debug.Log($"{hit.name} �÷��̾ Ÿ����");

            // �÷��̾� �ǰ� ó��
            hit.GetComponent<PlayerStat>().Hit(1);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, attackRadius);
    }
}
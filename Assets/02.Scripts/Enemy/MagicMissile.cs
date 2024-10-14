using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;              // �̻��� �̵� �ӵ�
    [SerializeField] private float rotationSpeed = 200f;     // �̻��� ȸ�� �ӵ�
    [SerializeField] private float lifeTime = 10.0f;         // �̻��� ���� �ð�
    [SerializeField] private int damage = 20;                // �̻����� �÷��̾�� ���ϴ� ������
    [SerializeField] private LayerMask targetLayer;          // Ž���� ���̾� (Player ���� Ÿ��)

    private Transform targetTransform;                       // Ÿ���� Transform
    private Rigidbody rb;                                    // �̻����� Rigidbody

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // ���� �ð��� ������ �̻��� �ı�
        Destroy(gameObject, lifeTime);

        // Ÿ�� Ž��
        FindTarget();
    }

    void Update()
    {
        if (targetTransform != null)
        {
            // Ÿ���� ���� ����
            Vector3 directionToTarget = (targetTransform.position - transform.position).normalized;
            Vector3 rotateAmount = Vector3.Cross(transform.forward, directionToTarget);

            // ȸ�� �ӵ� ����
            rb.angularVelocity = rotateAmount * rotationSpeed * Time.deltaTime;
            rb.velocity = transform.forward * speed;
        }
        else
        {
            // Ÿ���� ������ �����θ� �̵�
            rb.velocity = transform.forward * speed;
        }
    }

    void FindTarget()
    {
        // OverlapSphere�� Ÿ�� Ž��
        Collider[] hits = Physics.OverlapSphere(transform.position, 50f, targetLayer);

        if (hits.Length > 0)
        {
            targetTransform = hits[0].transform; // ���� ����� Ÿ������ ����
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // �̻����� �÷��̾�� �浹���� �� ������ �ֱ�
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerStat playerStat = other.GetComponent<PlayerStat>();
            Debug.Log("Fire");
            if (playerStat != null)
            {
                playerStat.Hit(damage); // �÷��̾�� ������ ������
            }

            // �浹 �� �̻��� �ı�
            Destroy(gameObject);
        }
    }

    // ����׿�: Ž�� �ݰ� �ð�ȭ
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 50f);
    }
}
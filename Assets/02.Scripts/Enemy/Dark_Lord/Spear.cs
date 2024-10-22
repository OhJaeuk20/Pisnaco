using System.Collections;
using UnityEngine;

public class Spear : MonoBehaviour
{
    public float speed = 10f; // â�� �ӵ�
    private Transform target; // �÷��̾� �Ǵ� ��ǥ
    private Rigidbody rb;
    public GameObject explosionPrefab; // ���� ������
    public Transform exPoint;
    public string groundLayerName; // Ground ���̾� �̸�
    public float explosionDelay; // ���� ���� �ð�

    void Start()
    {
        // Ÿ���� ã���ϴ�. ���⼭�� �÷��̾�� ����
        target = GameObject.FindWithTag("Player").transform;
        if (target != null)
        {
            // â�� �÷��̾� ������ ȸ����ŵ�ϴ�.
            Vector3 directionToPlayer = (target.position - transform.position).normalized;
            Quaternion rotationToPlayer = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = rotationToPlayer;

            // Rigidbody�� ���� â�� ��ǥ �������� ���� �����ϴ�.
            rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = directionToPlayer * speed;
            }
        }
    }

    // Ʈ���� �浹 ó��
    void OnTriggerEnter(Collider other)
    {
        // Ground ���̾�� �浹�ߴ��� Ȯ�� (�̸����� ������ ���̾�� ��)
        if (other.gameObject.layer == LayerMask.NameToLayer(groundLayerName))
        {
            rb.velocity = Vector3.zero;
            // ������ ������Ű�� �ڷ�ƾ ����
            StartCoroutine(ExplodeAfterDelay());
        }
    }

    // ���� ���� �ڷ�ƾ
    IEnumerator ExplodeAfterDelay()
    {
        // ���� �ð���ŭ ���
        yield return new WaitForSeconds(explosionDelay);

        // ���� ó�� �� â �ı�
        Instantiate(explosionPrefab, exPoint.position, Quaternion.identity);
        Destroy(gameObject); // â �ı�
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontPushPlayer : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // �浹�� ��ü�� 'Player' �±׸� ���� ���
        if (collision.gameObject.CompareTag("Player"))
        {
            // �浹�� ��ü�� Rigidbody ������Ʈ�� ������
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();

            // ���� Rigidbody�� �����Ѵٸ� �ӵ��� 0���� ����
            if (playerRb != null)
            {
                playerRb.velocity = Vector3.zero;
                playerRb.angularVelocity = Vector3.zero;
            }
        }
    }
}

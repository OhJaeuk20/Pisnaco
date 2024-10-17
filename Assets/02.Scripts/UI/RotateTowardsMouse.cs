using UnityEngine;

public class RotateTowardsMouse : MonoBehaviour
{
    void Update()
    {
        // 1. ���콺 ��ġ�κ��� Raycast ����
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // 2. Raycast�� ��� ��ġ�� ���
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // 3. ���콺�� ����Ű�� ������ ��ġ
            Vector3 targetPosition = hit.point;
            targetPosition.y = transform.position.y; // Y�� ���� (�������θ� ȸ��)

            // 4. LookAt�� ����Ͽ� �⺻ ȸ�� ����
            transform.LookAt(targetPosition);

            // 5. �ʱ� ȸ���� (-90, 0, 0)�� ����� ����
            transform.Rotate(-90, 0, 0);  // X�� -90��, Y�� 180�� ȸ��
        }
    }
}

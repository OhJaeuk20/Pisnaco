using UnityEngine;

public class ScaleOverTime : MonoBehaviour
{
    public float scaleSpeed = 1f; // �������� �����ϴ� �ӵ�

    private Vector3 initialScale; // �ʱ� ������ ��

    void Start()
    {
        // ������Ʈ�� �ʱ� ������ ����
        initialScale = transform.localScale;
    }

    void Update()
    {
        // �ð��� ����Ͽ� X�� Z �� ������ ����
        float scaleIncrease = scaleSpeed * Time.deltaTime;

        // X�� Z �࿡�� ���� (Y�� ����)
        transform.localScale = new Vector3(
            transform.localScale.x + scaleIncrease,
            initialScale.y,
            transform.localScale.z + scaleIncrease
        );
    }
}
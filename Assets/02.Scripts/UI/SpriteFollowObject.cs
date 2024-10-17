using UnityEngine;

public class SpriteFollowObject : MonoBehaviour
{
    private GameObject targetObject; // ����ٴ� ������Ʈ
    public Vector3 offset; // ������Ʈ �������� ��������Ʈ�� ��ġ ������

    private void Start()
    {
        targetObject = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        // Ÿ�� ������Ʈ�� ��ġ�� �������� ���Ͽ� ��������Ʈ ��ġ�� ������Ʈ
        transform.position = targetObject.transform.position + offset;
    }
}
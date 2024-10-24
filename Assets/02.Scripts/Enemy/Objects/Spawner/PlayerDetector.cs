using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    private MonsterSpawner spawner;

    void Start()
    {
        // �θ� ������Ʈ�� MonsterSpawner ��ũ��Ʈ�� ã��
        spawner = GetComponentInParent<MonsterSpawner>();
    }

    void OnTriggerEnter(Collider other)
    {
        // �÷��̾ �����Ǹ� �θ��� OnPlayerEnter �޼��� ȣ��
        if (other.CompareTag("Player"))
        {
            spawner.OnPlayerEnter();
        }
    }
}
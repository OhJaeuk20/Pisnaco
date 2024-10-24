using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    private MonsterSpawner spawner;

    void Start()
    {
        // 부모 오브젝트의 MonsterSpawner 스크립트를 찾음
        spawner = GetComponentInParent<MonsterSpawner>();
    }

    void OnTriggerEnter(Collider other)
    {
        // 플레이어가 감지되면 부모의 OnPlayerEnter 메서드 호출
        if (other.CompareTag("Player"))
        {
            spawner.OnPlayerEnter();
        }
    }
}
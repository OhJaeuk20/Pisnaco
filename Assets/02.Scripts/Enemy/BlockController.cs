using UnityEngine;

public class BlockController : MonoBehaviour
{
    public GameObject[] blocks;  // 길목을 막을 게임 오브젝트들 (미리 비활성화해 두기)
    private bool isActivated = false;
    private MonsterSpawner monsterSpawner;

    void Start()
    {
        monsterSpawner = GetComponentInParent<MonsterSpawner>();
    }

    void OnTriggerEnter(Collider other)
    {
        // 플레이어가 트리거에 진입할 때
        if (other.CompareTag("Player") && !isActivated)
        {
            ActivateBlocks();
        }
    }

    void ActivateBlocks()
    {
        isActivated = true;

        // 배열에 있는 게임 오브젝트들 활성화
        foreach (GameObject block in blocks)
        {
            block.SetActive(true);
        }
    }

    public void DeactivateBlocks()
    {
        // 특정 조건이 달성되었을 때 배열에 있는 오브젝트들을 비활성화
        foreach (GameObject block in blocks)
        {
            block.SetActive(false);
        }

        isActivated = false;  // 다시 비활성화된 상태로 전환
    }

    // 예시: 특정 조건 달성 체크
    void Update()
    {
        // 특정 조건을 달성하면 길목을 막고 있는 오브젝트들을 비활성화 (여기에 조건 로직을 넣음)
        if (CheckIfConditionMet())
        {
            DeactivateBlocks();
        }
    }

    bool CheckIfConditionMet()
    {
        if (monsterSpawner.waveEnd)
            return true;
        return false;  // 조건을 만족하면 true를 반환
    }
}
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public GameObject[] blocks;  // ����� ���� ���� ������Ʈ�� (�̸� ��Ȱ��ȭ�� �α�)
    private bool isActivated = false;
    private MonsterSpawner monsterSpawner;

    void Start()
    {
        monsterSpawner = GetComponentInParent<MonsterSpawner>();
    }

    void OnTriggerEnter(Collider other)
    {
        // �÷��̾ Ʈ���ſ� ������ ��
        if (other.CompareTag("Player") && !isActivated)
        {
            ActivateBlocks();
        }
    }

    void ActivateBlocks()
    {
        isActivated = true;

        // �迭�� �ִ� ���� ������Ʈ�� Ȱ��ȭ
        foreach (GameObject block in blocks)
        {
            block.SetActive(true);
        }
    }

    public void DeactivateBlocks()
    {
        // Ư�� ������ �޼��Ǿ��� �� �迭�� �ִ� ������Ʈ���� ��Ȱ��ȭ
        foreach (GameObject block in blocks)
        {
            block.SetActive(false);
        }

        isActivated = false;  // �ٽ� ��Ȱ��ȭ�� ���·� ��ȯ
    }

    // ����: Ư�� ���� �޼� üũ
    void Update()
    {
        // Ư�� ������ �޼��ϸ� ����� ���� �ִ� ������Ʈ���� ��Ȱ��ȭ (���⿡ ���� ������ ����)
        if (CheckIfConditionMet())
        {
            DeactivateBlocks();
        }
    }

    bool CheckIfConditionMet()
    {
        if (monsterSpawner.waveEnd)
            return true;
        return false;  // ������ �����ϸ� true�� ��ȯ
    }
}
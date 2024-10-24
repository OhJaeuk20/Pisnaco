using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject[] wave1Monsters;
    public GameObject[] wave2Monsters;
    public Transform[] spawnPoints;
    public GameObject spawnEffectPrefab;
    private int currentWave = 0;
    private bool waveStarted = false;
    private int monsterCount = 0; // ���� �ݶ��̴� �ȿ� �ִ� ���� ��
    public bool waveEnd = false;

    public void OnPlayerEnter()
    {
        if (!waveStarted)
        {
            waveStarted = true;
            StartWave(1);  // ù ��° ���̺� ����
        }
    }

    void Update()
    {
        Debug.Log($"{monsterCount} , {currentWave}");
        // ���̺갡 ���۵� ���¿��� ���Ͱ� �׾����� üũ
        if (waveStarted && monsterCount == 0)
        {
            if (currentWave == 1)
            {
                Debug.Log("Wave 1 complete. Starting Wave 2...");
                StartWave(2);  // �� ��° ���̺� ����
            }
            else if (currentWave == 2)
            {
                Debug.Log("All waves completed!");
                waveEnd = true;
            }
        }
    }

    void StartWave(int wave)
    {
        currentWave = wave;

        if (wave == 1)
        {
            SpawnMonsters(wave1Monsters);
        }
        else if (wave == 2)
        {
            SpawnMonsters(wave2Monsters);
        }
    }

    void SpawnMonsters(GameObject[] monsters)
    {
        for (int i = 0; i < monsters.Length; i++)
        {
            Instantiate(spawnEffectPrefab, spawnPoints[i].position, Quaternion.identity);
            GameObject monster = Instantiate(monsters[i], spawnPoints[i].position, Quaternion.identity);
            monster.GetComponent<MonsterFSMInfo>().DetectDistance = 100;
            monster.GetComponent<MonsterHealth>().spawner = this;

            // ���Ͱ� �����Ǹ� monsterCount ����
            monsterCount++;
        }
    }

    public void MonsterDiscount()
    {
        monsterCount--;
    }
}

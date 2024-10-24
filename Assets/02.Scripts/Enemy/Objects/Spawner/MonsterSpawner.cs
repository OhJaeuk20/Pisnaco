using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject[] wave1Monsters;
    public GameObject[] wave2Monsters;
    public Transform[] spawnPoints;
    public GameObject spawnEffectPrefab;
    private int currentWave = 0;
    private bool waveStarted = false;
    private int monsterCount = 0; // 현재 콜라이더 안에 있는 몬스터 수
    public bool waveEnd = false;

    public void OnPlayerEnter()
    {
        if (!waveStarted)
        {
            waveStarted = true;
            StartWave(1);  // 첫 번째 웨이브 시작
        }
    }

    void Update()
    {
        Debug.Log($"{monsterCount} , {currentWave}");
        // 웨이브가 시작된 상태에서 몬스터가 죽었는지 체크
        if (waveStarted && monsterCount == 0)
        {
            if (currentWave == 1)
            {
                Debug.Log("Wave 1 complete. Starting Wave 2...");
                StartWave(2);  // 두 번째 웨이브 시작
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

            // 몬스터가 생성되면 monsterCount 증가
            monsterCount++;
        }
    }

    public void MonsterDiscount()
    {
        monsterCount--;
    }
}

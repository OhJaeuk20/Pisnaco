using UnityEngine;
using UnityEngine.SceneManagement;

public class Pisnaco_PlayerManager : MonoBehaviour
{
    public GameObject playerPrefab;
    private GameObject playerInstance;

    // 플레이어가 생성되어야 하는 씬 이름들을 배열로 지정
    public string[] gameplayScenes;

    private void Awake()
    {
        // 현재 씬이 게임플레이 씬인 경우에만 플레이어 초기화
        if (ShouldSpawnPlayer())
        {
            InitializePlayer();
        }
    }

    private bool ShouldSpawnPlayer()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        // gameplayScenes 배열에 현재 씬 이름이 포함되어 있는지 확인
        foreach (string sceneName in gameplayScenes)
        {
            if (currentSceneName == sceneName)
            {
                return true;
            }
        }
        return false;
    }

    private void InitializePlayer()
    {
        if (playerInstance == null)
        {
            playerInstance = GameObject.FindGameObjectWithTag("Player");
            if (playerInstance == null && playerPrefab != null)
            {
                playerInstance = Instantiate(playerPrefab);
                Debug.Log("새로운 플레이어 인스턴스가 생성되었습니다.");
            }
        }
    }

    public void SetPlayerPosition(Vector3 newPosition)
    {
        if (playerInstance == null && ShouldSpawnPlayer())
        {
            InitializePlayer();
        }
        if (playerInstance != null)
        {
            playerInstance.transform.position = newPosition;
            Debug.Log($"플레이어 위치가 설정됨: {newPosition}");
        }
        else
        {
            Debug.LogError("플레이어 인스턴스가 없어 위치를 설정할 수 없습니다.");
        }
    }

    public GameObject GetPlayer()
    {
        if (playerInstance == null && ShouldSpawnPlayer())
        {
            InitializePlayer();
        }
        return playerInstance;
    }
}
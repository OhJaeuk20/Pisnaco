using UnityEngine;
using UnityEngine.SceneManagement;

public class Pisnaco_PlayerManager : MonoBehaviour
{
    public GameObject playerPrefab;
    private GameObject playerInstance;

    // �÷��̾ �����Ǿ�� �ϴ� �� �̸����� �迭�� ����
    public string[] gameplayScenes;

    private void Awake()
    {
        // ���� ���� �����÷��� ���� ��쿡�� �÷��̾� �ʱ�ȭ
        if (ShouldSpawnPlayer())
        {
            InitializePlayer();
        }
    }

    private bool ShouldSpawnPlayer()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        // gameplayScenes �迭�� ���� �� �̸��� ���ԵǾ� �ִ��� Ȯ��
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
                Debug.Log("���ο� �÷��̾� �ν��Ͻ��� �����Ǿ����ϴ�.");
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
            Debug.Log($"�÷��̾� ��ġ�� ������: {newPosition}");
        }
        else
        {
            Debug.LogError("�÷��̾� �ν��Ͻ��� ���� ��ġ�� ������ �� �����ϴ�.");
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
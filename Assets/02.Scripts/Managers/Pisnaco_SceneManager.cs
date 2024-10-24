using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class Pisnaco_SceneManager : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void LoadNextScene(string sceneName)
    {
        // GameManager�� ���� �÷��̾� Ȯ��
        if (GameManager.Instance.GetPlayer() == null)
        {
            Debug.LogWarning("�� ��ȯ �� �÷��̾ �����ϴ�. PlayerManager�� Ȯ���ϼ���.");
        }
        SceneManager.LoadScene(sceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(SetupPlayerAfterSceneLoad());
    }

    private IEnumerator SetupPlayerAfterSceneLoad()
    {
        yield return new WaitForEndOfFrame();
        SetPlayerSpawnPosition();

        GameObject player = GameManager.Instance.GetPlayer();
        if (player != null)
        {
            GameManager.Instance.InitializeCamera(player.transform);
        }
        else
        {
            Debug.LogWarning("�÷��̾� ��ü�� ã�� �� �����ϴ�.");
        }
    }

    private void SetPlayerSpawnPosition()
    {
        GameObject spawnPoint = GameObject.FindGameObjectWithTag("PSPAWN");
        if (spawnPoint != null)
        {
            Debug.Log($"���� ����Ʈ �߰�: {spawnPoint.transform.position}");
            GameManager.Instance.SetPlayerPosition(spawnPoint.transform.position);
        }
        else
        {
            Debug.LogWarning("���� ����Ʈ�� ã�� �� �����ϴ�.");
        }
    }
}
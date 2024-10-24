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
        // GameManager를 통해 플레이어 확인
        if (GameManager.Instance.GetPlayer() == null)
        {
            Debug.LogWarning("씬 전환 전 플레이어가 없습니다. PlayerManager를 확인하세요.");
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
            Debug.LogWarning("플레이어 객체를 찾을 수 없습니다.");
        }
    }

    private void SetPlayerSpawnPosition()
    {
        GameObject spawnPoint = GameObject.FindGameObjectWithTag("PSPAWN");
        if (spawnPoint != null)
        {
            Debug.Log($"스폰 포인트 발견: {spawnPoint.transform.position}");
            GameManager.Instance.SetPlayerPosition(spawnPoint.transform.position);
        }
        else
        {
            Debug.LogWarning("스폰 포인트를 찾을 수 없습니다.");
        }
    }
}
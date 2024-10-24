using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // 매니저들의 프로퍼티
    public Pisnaco_PlayerManager PlayerManager { get; private set; }
    public Pisnaco_CameraManager CameraManager { get; private set; }
    public Pisnaco_SceneManager SceneManager { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeManagers();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeManagers()
    {
        // 매니저 컴포넌트들 가져오기
        PlayerManager = GetComponent<Pisnaco_PlayerManager>();
        CameraManager = GetComponent<Pisnaco_CameraManager>();
        SceneManager = GetComponent<Pisnaco_SceneManager>();

        // 매니저들이 제대로 할당되었는지 확인
        if (PlayerManager == null)
            Debug.LogError("PlayerManager가 GameManager 오브젝트에 없습니다!");
        if (CameraManager == null)
            Debug.LogError("CameraManager가 GameManager 오브젝트에 없습니다!");
        if (SceneManager == null)
            Debug.LogError("SceneManager가 GameManager 오브젝트에 없습니다!");
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    // 게임 전체에서 사용할 수 있는 유틸리티 메서드들
    public void LoadScene(string sceneName)
    {
        SceneManager?.LoadNextScene(sceneName);
    }

    public GameObject GetPlayer()
    {
        return PlayerManager?.GetPlayer();
    }

    public void SetPlayerPosition(Vector3 position)
    {
        PlayerManager?.SetPlayerPosition(position);
    }

    public void InitializeCamera(Transform playerTransform)
    {
        CameraManager?.InitializeCamera(playerTransform);
    }
}
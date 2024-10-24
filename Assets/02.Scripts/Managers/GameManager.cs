using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // �Ŵ������� ������Ƽ
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
        // �Ŵ��� ������Ʈ�� ��������
        PlayerManager = GetComponent<Pisnaco_PlayerManager>();
        CameraManager = GetComponent<Pisnaco_CameraManager>();
        SceneManager = GetComponent<Pisnaco_SceneManager>();

        // �Ŵ������� ����� �Ҵ�Ǿ����� Ȯ��
        if (PlayerManager == null)
            Debug.LogError("PlayerManager�� GameManager ������Ʈ�� �����ϴ�!");
        if (CameraManager == null)
            Debug.LogError("CameraManager�� GameManager ������Ʈ�� �����ϴ�!");
        if (SceneManager == null)
            Debug.LogError("SceneManager�� GameManager ������Ʈ�� �����ϴ�!");
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    // ���� ��ü���� ����� �� �ִ� ��ƿ��Ƽ �޼����
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
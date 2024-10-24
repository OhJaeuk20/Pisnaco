using UnityEngine;

public class Portal : MonoBehaviour
{
    public string sceneToLoad; // ��ȯ�� ���� �̸�

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // �÷��̾�� �浹�ߴ��� Ȯ��
        {
            Pisnaco_SceneManager sceneManager = FindObjectOfType<Pisnaco_SceneManager>();
            if (sceneManager != null)
            {
                sceneManager.LoadNextScene(sceneToLoad); // �� ��ȯ ȣ��
            }
        }
    }
}
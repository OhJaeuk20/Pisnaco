using UnityEngine;

public class Portal : MonoBehaviour
{
    public string sceneToLoad; // 전환할 씬의 이름

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어와 충돌했는지 확인
        {
            Pisnaco_SceneManager sceneManager = FindObjectOfType<Pisnaco_SceneManager>();
            if (sceneManager != null)
            {
                sceneManager.LoadNextScene(sceneToLoad); // 씬 전환 호출
            }
        }
    }
}
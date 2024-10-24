using UnityEngine;
using Cinemachine;

public class Pisnaco_CameraManager : MonoBehaviour
{
    private CinemachineVirtualCamera playerCamera; // �÷��̾ ����ٴ� ī�޶�

    private void Awake()
    {
        // Awake���� ī�޶� ã�� �ʰ�, InitializeCamera���� ã�ƾ� ��
    }

    public void InitializeCamera(Transform playerTransform)
    {
        // �±װ� "PlayerCamera"�� ī�޶� ã��
        playerCamera = GameObject.FindGameObjectWithTag("PLAYERCAMERA")?.GetComponent<CinemachineVirtualCamera>();

        if (playerCamera == null)
        {
            Debug.LogWarning("PlayerCamera �±׸� ���� CinemachineVirtualCamera�� ã�� �� �����ϴ�.");
            return;
        }

        Transform lookPos = playerTransform?.Find("LookPos"); // LookPos �ڽ� ã��
        SetCameraTargets(playerTransform, lookPos); // ī�޶� ����
    }

    private void SetCameraTargets(Transform playerTransform, Transform lookPosTransform)
    {
        if (playerCamera != null)
        {
            playerCamera.Follow = playerTransform; // ī�޶� Follow ����

            if (lookPosTransform != null)
            {
                playerCamera.LookAt = lookPosTransform; // LookAt ����
            }
            else
            {
                Debug.LogWarning("LookPos�� ã�� �� �����ϴ�.");
            }
        }
    }
}
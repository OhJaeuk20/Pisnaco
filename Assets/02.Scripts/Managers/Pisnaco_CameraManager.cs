using UnityEngine;
using Cinemachine;

public class Pisnaco_CameraManager : MonoBehaviour
{
    private CinemachineVirtualCamera playerCamera; // 플레이어를 따라다닐 카메라

    private void Awake()
    {
        // Awake에서 카메라를 찾지 않고, InitializeCamera에서 찾아야 함
    }

    public void InitializeCamera(Transform playerTransform)
    {
        // 태그가 "PlayerCamera"인 카메라 찾기
        playerCamera = GameObject.FindGameObjectWithTag("PLAYERCAMERA")?.GetComponent<CinemachineVirtualCamera>();

        if (playerCamera == null)
        {
            Debug.LogWarning("PlayerCamera 태그를 가진 CinemachineVirtualCamera를 찾을 수 없습니다.");
            return;
        }

        Transform lookPos = playerTransform?.Find("LookPos"); // LookPos 자식 찾기
        SetCameraTargets(playerTransform, lookPos); // 카메라 설정
    }

    private void SetCameraTargets(Transform playerTransform, Transform lookPosTransform)
    {
        if (playerCamera != null)
        {
            playerCamera.Follow = playerTransform; // 카메라 Follow 설정

            if (lookPosTransform != null)
            {
                playerCamera.LookAt = lookPosTransform; // LookAt 설정
            }
            else
            {
                Debug.LogWarning("LookPos를 찾을 수 없습니다.");
            }
        }
    }
}
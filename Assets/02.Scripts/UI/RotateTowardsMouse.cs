using UnityEngine;

public class RotateTowardsMouse : MonoBehaviour
{
    void Update()
    {
        // 1. 마우스 위치로부터 Raycast 생성
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // 2. Raycast가 닿는 위치를 계산
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // 3. 마우스가 가리키는 지점의 위치
            Vector3 targetPosition = hit.point;
            targetPosition.y = transform.position.y; // Y축 고정 (수평으로만 회전)

            // 4. LookAt을 사용하여 기본 회전 설정
            transform.LookAt(targetPosition);

            // 5. 초기 회전값 (-90, 0, 0)을 고려한 보정
            transform.Rotate(-90, 0, 0);  // X축 -90도, Y축 180도 회전
        }
    }
}

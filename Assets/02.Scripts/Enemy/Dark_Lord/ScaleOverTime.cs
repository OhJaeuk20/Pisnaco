using UnityEngine;

public class ScaleOverTime : MonoBehaviour
{
    public float scaleSpeed = 1f; // 스케일이 증가하는 속도

    private Vector3 initialScale; // 초기 스케일 값

    void Start()
    {
        // 오브젝트의 초기 스케일 저장
        initialScale = transform.localScale;
    }

    void Update()
    {
        // 시간에 비례하여 X와 Z 축 스케일 증가
        float scaleIncrease = scaleSpeed * Time.deltaTime;

        // X와 Z 축에만 적용 (Y는 유지)
        transform.localScale = new Vector3(
            transform.localScale.x + scaleIncrease,
            initialScale.y,
            transform.localScale.z + scaleIncrease
        );
    }
}
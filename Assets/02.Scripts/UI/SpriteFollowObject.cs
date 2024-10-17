using UnityEngine;

public class SpriteFollowObject : MonoBehaviour
{
    private GameObject targetObject; // 따라다닐 오브젝트
    public Vector3 offset; // 오브젝트 기준으로 스프라이트의 위치 오프셋

    private void Start()
    {
        targetObject = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        // 타겟 오브젝트의 위치에 오프셋을 더하여 스프라이트 위치를 업데이트
        transform.position = targetObject.transform.position + offset;
    }
}
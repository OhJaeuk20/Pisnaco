using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;
    public float rotationAngleX = 45f;

    private float speed = 10.0f;

    void Start()
    {
        transform.rotation = Quaternion.Euler(rotationAngleX, 0, 0);
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;

        // Lerp�� ����Ͽ� ī�޶� ��ġ�� �ε巴�� �̵�
        //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * speed);

        // ī�޶� ��ġ ������Ʈ
        transform.position = smoothedPosition;
    }
}

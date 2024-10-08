using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontPushPlayer : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 객체가 'Player' 태그를 가진 경우
        if (collision.gameObject.CompareTag("Player"))
        {
            // 충돌한 객체의 Rigidbody 컴포넌트를 가져옴
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();

            // 만약 Rigidbody가 존재한다면 속도를 0으로 설정
            if (playerRb != null)
            {
                playerRb.velocity = Vector3.zero;
                playerRb.angularVelocity = Vector3.zero;
            }
        }
    }
}

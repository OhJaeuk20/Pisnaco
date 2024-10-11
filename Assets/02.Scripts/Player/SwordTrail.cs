using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordTrail : MonoBehaviour
{
    public GameObject effectPrefab;

    // 이펙트가 소환될 위치와 각도를 설정하는 변수
    public GameObject spawnPoint;

    public void SpawnEffect1()
    {
        if (effectPrefab != null)
        {
            Quaternion spawnRotation = spawnPoint.transform.rotation * Quaternion.Euler(-90, 90, 0);
            // Quaternion.Euler로 각도값을 쿼터니언으로 변환하여 회전값 설정
            Instantiate(effectPrefab, spawnPoint.transform.position, spawnRotation);
        }
    }

    public void SpawnEffect2()
    {
        if (effectPrefab != null)
        {
            Quaternion spawnRotation = spawnPoint.transform.rotation * Quaternion.Euler(90, 90, 0);
            // Quaternion.Euler로 각도값을 쿼터니언으로 변환하여 회전값 설정
            Instantiate(effectPrefab, spawnPoint.transform.position, spawnRotation);
        }
    }
}
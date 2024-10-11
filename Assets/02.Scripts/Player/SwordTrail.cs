using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordTrail : MonoBehaviour
{
    public GameObject effectPrefab;

    // ����Ʈ�� ��ȯ�� ��ġ�� ������ �����ϴ� ����
    public GameObject spawnPoint;

    public void SpawnEffect1()
    {
        if (effectPrefab != null)
        {
            Quaternion spawnRotation = spawnPoint.transform.rotation * Quaternion.Euler(-90, 90, 0);
            // Quaternion.Euler�� �������� ���ʹϾ����� ��ȯ�Ͽ� ȸ���� ����
            Instantiate(effectPrefab, spawnPoint.transform.position, spawnRotation);
        }
    }

    public void SpawnEffect2()
    {
        if (effectPrefab != null)
        {
            Quaternion spawnRotation = spawnPoint.transform.rotation * Quaternion.Euler(90, 90, 0);
            // Quaternion.Euler�� �������� ���ʹϾ����� ��ȯ�Ͽ� ȸ���� ����
            Instantiate(effectPrefab, spawnPoint.transform.position, spawnRotation);
        }
    }
}
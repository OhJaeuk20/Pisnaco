using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyer : MonoBehaviour
{
    public float destroyTime;  // ��ƼŬ�� �ı��� �ð� (��)

    void Start()
    {
        // Ư�� �ð� �Ŀ� ��ƼŬ ������Ʈ�� �ı�
        Destroy(gameObject, destroyTime);
    }
}

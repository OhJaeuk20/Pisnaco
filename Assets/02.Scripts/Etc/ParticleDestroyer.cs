using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyer : MonoBehaviour
{
    public float destroyTime;  // 파티클이 파괴될 시간 (초)

    void Start()
    {
        // 특정 시간 후에 파티클 오브젝트를 파괴
        Destroy(gameObject, destroyTime);
    }
}

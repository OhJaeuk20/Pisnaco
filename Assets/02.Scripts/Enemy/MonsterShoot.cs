using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterShoot : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;

    // 공격 타겟 중심점 위치
    [SerializeField] private Transform attackTransform;

    // 공격 애니메이션 피격 이벤트
    public void ShootAnimationEvent()
    {
        Instantiate(arrowPrefab, attackTransform.position, attackTransform.rotation);
    }
}

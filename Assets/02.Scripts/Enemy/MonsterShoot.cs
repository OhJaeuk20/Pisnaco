using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterShoot : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;

    // ���� Ÿ�� �߽��� ��ġ
    [SerializeField] private Transform attackTransform;

    // ���� �ִϸ��̼� �ǰ� �̺�Ʈ
    public void ShootAnimationEvent()
    {
        Instantiate(arrowPrefab, attackTransform.position, attackTransform.rotation);
    }
}
